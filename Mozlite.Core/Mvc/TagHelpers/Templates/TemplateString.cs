using System;
using System.Linq;
using System.Text;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// �ַ�����
    /// </summary>
    public class TemplateString
    {
        private readonly string _source;
        private int _index;
        /// <summary>
        /// ��ʼ����<see cref="TemplateString"/>��
        /// </summary>
        /// <param name="source">��ǰ�ַ�����</param>
        public TemplateString(string source)
        {
            _source = source;
        }

        /// <summary>
        /// ��ȡֱ������<paramref name="curs"/>�ַ�֮һ����()�����ַ������ȶ�ȡ��
        /// </summary>
        /// <param name="curs">��ǰ�ַ���</param>
        /// <returns>���ض�ȡ���ַ�����</returns>
        public string ReadUntil(params char[] curs)
        {
            var sb = new StringBuilder();
            while (CanRead)
            {
                if (curs.Contains(Current))
                {
                    _index++;
                    return sb.ToString();
                }
                if (Current == '\'' || Current == '"')
                {
                    sb.Append(ReadQuote(Current));
                }
                else if (Current == '(')
                {
                    sb.Append("(").Append(ReadBlock()).Append(')');
                }
                else
                {
                    sb.Append(Current);
                }
                _index++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// ��ȡֱ���������������ַ���
        /// </summary>
        /// <param name="cur">��ǰ�ַ���</param>
        /// <param name="next">��һ���ַ���</param>
        /// <returns>���ض�ȡ���ַ�����</returns>
        public string ReadNext(char cur, char next)
        {
            var sb = new StringBuilder();
            while (CanRead)
            {
                if (Current == cur && Next == next)
                {
                    _index += 2;
                    return sb.ToString();
                }
                if (Current == '\'' || Current == '"')
                {
                    sb.Append(ReadQuote(Current));
                }
                else if (Current == '(')
                {
                    sb.Append("(").Append(ReadBlock()).Append(')');
                }
                else
                {
                    sb.Append(Current);
                }
                _index++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// ��ʼ��ȡ����λ����Ϊ0��
        /// </summary>
        public void Begin()
        {
            _index = 0;
        }

        /// <summary>
        /// ��ȡֱ������<paramref name="cur"/>�ַ�����()�����ַ������ȶ�ȡ��
        /// </summary>
        /// <param name="cur">��ǰ�ַ���</param>
        /// <returns>���ض�ȡ���ַ�����</returns>
        public string ReadUntil(char cur)
        {
            var sb = new StringBuilder();
            while (CanRead)
            {
                if (Current == cur)
                {
                    _index++;
                    return sb.ToString();
                }
                if (Current == '\'' || Current == '"')
                {
                    sb.Append(ReadQuote(Current));
                }
                else if (Current == '(')
                {
                    sb.Append("(").Append(ReadBlock()).Append(')');
                }
                else
                {
                    sb.Append(Current);
                }
                _index++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// ��ȡ������<paramref name="text"/>�е��ַ���
        /// </summary>
        /// <param name="text">�Ϸ��ַ����ϡ�</param>
        /// <returns>���ض�ȡ���ַ�����</returns>
        public string Read(string text)
        {
            var sb = new StringBuilder();
            while (CanRead)
            {
                if (!text.Contains(Current))
                    break;
                sb.Append(Current);
                _index++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// �жϵ�ǰλ���Ƿ�Ϸ���
        /// </summary>
        /// <returns>�����жϽ�����������<c>false</c>�����ʾ����ĩβ��</returns>
        public bool CanRead => _index < _source.Length;

        /// <summary>
        /// ���˿ո�
        /// </summary>
        public void PassOptionalWhitespace()
        {
            while (CanRead && (Current == ' ' || Current == '\t' || Current == '\r' || Current == '\n'))
            {
                _index++;
            }
        }

        /// <summary>
        /// ��ȡ���š�
        /// </summary>
        public string ReadBlock()
        {
            var sb = new StringBuilder();
            _index++;
            var block = 1;
            while (CanRead)
            {
                switch (Current)
                {
                    case '"':
                    case '\'':
                        sb.Append(ReadQuote(Current));
                        continue;
                    case '(':
                        block++;
                        break;
                    case ')':
                        block--;
                        break;
                }
                if (block == 0)
                {
                    return sb.ToString();
                }
                sb.Append(Current);
                _index++;
            }
            throw new Exception("�﷨����û��������)�����������");
        }

        /// <summary>
        /// ��ȡ�����ַ�����
        /// </summary>
        public StringBuilder ReadQuote(char quote)
        {
            /*
                xx == 'sdf{{sdfsdf';
                xx == 'sdf\'sd{{}}fsdf';
                xx == 'sdfsd{{}}fsdf\\';
             */
            var sb = new StringBuilder();
            sb.Append(quote);
            _index++;
            while (CanRead)
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
        /// ��������λ��
        /// </summary>
        /// <param name="length">���ȡ�</param>
        public void Skip(int length = 1)
        {
            _index += length;
        }

        /// <summary>
        /// ������ַ���
        /// </summary>
        public const char None = '\0';

        /// <summary>
        /// ��ǰ�ַ���
        /// </summary>
        public char Current => _source[_index];

        /// <summary>
        /// ��һ���ַ���
        /// </summary>
        public char Next => _index + 1 < _source.Length ? _source[_index + 1] : None;

        /// <summary>���ر�ʾ��ǰ������ַ�����</summary>
        /// <returns>��ʾ��ǰ������ַ�����</returns>
        public override string ToString() => _source.Substring(_index);
    }
}