using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Mozlite.Mvc.TagHelpers.Ajax
{
    /// <summary>
    /// ����㡣
    /// </summary>
    public class DocumentElement : Element
    {
        private static readonly Regex _regex = new Regex("<script.*?>(.*?)</script>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private readonly List<string> _scripts = new List<string>();
        private readonly string _source;
        private int _index;
        private readonly int _maxIndex;
        /// <summary>
        /// ��ʼ����<see cref="DocumentElement"/>��
        /// </summary>
        /// <param name="source">Դ���롣</param>
        public DocumentElement(string source)
            : base(source, ElementType.Doc)
        {
            //��ȡ�ű�
            source = _regex.Replace(source, match =>
            {
                _scripts.Add(match.Groups[1].Value.Trim().Trim(';'));
                return null;
            });
            _source = source;
            _maxIndex = _source.Length - 1;
            Read(this);
        }

        private char Current => _source[_index];

        private char? Next
        {
            get
            {
                if (_source.Length > _index + 1)
                    return _source[_index + 1];
                return null;
            }
        }

        /// <summary>
        /// ���ش��롣
        /// </summary>
        private void Read(Element element)
        {
            while (_maxIndex > _index)
            {
                if (Current == '{' && Next == '{')
                {
                    if (ReadCodeBlock(element))
                        break;
                }
                else
                    ReadTextBlock(element);
            }
        }

        /// <summary>
        /// ��ȡHTML���롣
        /// </summary>
        /// <param name="parent">�����ڵ㡣</param>
        private void ReadTextBlock(Element parent)
        {
            var sb = new StringBuilder();
            while (_maxIndex > _index)
            {
                if (Current == '{' && Next == '{')
                    break;
                sb.Append(Current);
                _index++;
            }
            parent.Add(new TextElement(sb.ToString()));
        }

        /// <summary>
        /// ��ȡ�����ַ�����
        /// </summary>
        private StringBuilder ReadQuote(char quote)
        {
            /*
                xx == 'sdf{{sdfsdf';
                xx == 'sdf\'sd{{}}fsdf';
                xx == 'sdfsd{{}}fsdf\\';
             */
            var sb = new StringBuilder();
            sb.Append(quote);
            _index++;
            while (_maxIndex > _index)
            {
                if (quote == Current)
                    break;
                if (Current == '\\')//ת�����
                {
                    sb.Append(Current);
                    _index++;
                    continue;
                }
                sb.Append(Current);
                _index++;
            }
            sb.Append(quote);
            _index++;
            return sb;
        }

        /// <summary>
        /// ��ȡ����Ƭ�Ρ�
        /// </summary>
        private string ReadCode()
        {
            var sb = new StringBuilder();
            _index += 2;//ȥ��{{
            while (_maxIndex > _index)
            {
                if (Current == '\'' || Current == '"')
                {
                    sb.Append(ReadQuote(Current));
                    continue;
                }
                if (Current == '}' && Next == '}')
                    break;
                sb.Append(Current);
                _index++;
            }
            _index += 2;//ȥ��}}
            return sb.ToString();
        }

        /// <summary>
        /// ��ȡ����顣
        /// </summary>
        /// <param name="parent">����ʵ����</param>
        /// <returns>���������ȡ����<c>true</c>��</returns>
        private bool ReadCodeBlock(Element parent)
        {
            var code = ReadCode().Trim();
            if (code.Length == 0)
                return true;
            if (code[0] == '/')
            {//���������
                code = code.Substring(1).Trim();
                if (parent is CodeBlockElement block && block.Key != code)
                    throw new Exception($"{code}�﷨���󣬽���������ɶ�Ƕ��ʹ�ã�");
                return true;
            }
            var index = code.IndexOf(' ');
            if (index == -1)
            {
                parent.Add(new CodeElement(code));
                return false;
            }
            var key = code.Substring(0, index);
            code = code.Substring(index).Trim(' ', '(', ')');
            var current = new CodeBlockElement(key, code);
            parent.Add(current);
            Read(current);
            return false;
        }

        /// <summary>
        /// ��ȡ�ű���
        /// </summary>
        public List<string> Scripts => _scripts;
    }
}