using System;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// ����ڵ㡣
    /// </summary>
    public class TemplateCodeElement : TemplateElementBase
    {
        /// <summary>
        /// ��ʼ����<see cref="TemplateCodeElement"/>��
        /// </summary>
        /// <param name="source">Դ���롣</param>
        /// <param name="position">λ�á�</param>
        protected internal TemplateCodeElement(string source, int position) : base(position, TemplateElementType.Code)
        {
            var index = source.IndexOf(' ');
            if (index == -1)
            { Keyword = source; }
            else
            {
                Keyword = source.Substring(0, index);
                Args = source.Substring(index + 1).Trim();
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string Args { get; }

        /// <summary>
        /// ���ؼ��֡�
        /// </summary>
        public string Keyword { get; }

        /// <summary>���ر�ʾ��ǰ������ַ�����</summary>
        /// <returns>��ʾ��ǰ������ַ�����</returns>
        public override string ToString()
        {
            if (Args == null) return "{{" + Keyword + "}}";
            return $"{{{Keyword} {Args}}}";
        }

        /// <summary>
        /// ���ɽű���
        /// </summary>
        /// <param name="executor">�ű��﷨��������</param>
        /// <returns>�������ɺ�Ľű���</returns>
        public override string ToJsString(ITemplateExecutor executor)
        {
            if (Args == null)
                return $"html+= {Keyword};";
            return $"html+= {Keyword} {Args};";
        }

        /// <summary>
        /// ����HTML���롣
        /// </summary>
        /// <param name="executor">�﷨��������</param>
        /// <param name="instance">��ǰʵ����</param>
        /// <param name="func">��ȡʵ������ֵ��</param>
        /// <returns>����HTML���롣</returns>
        public override string ToHtmlString(ITemplateExecutor executor, object instance, Func<object, string, object> func)
        {
            return func(instance, Keyword)?.ToString();
        }
    }
}