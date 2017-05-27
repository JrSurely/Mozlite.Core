using System;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// �ı��ڵ㡣
    /// </summary>
    public class TemplateTextElement : TemplateElementBase
    {
        private readonly string _source;
        /// <summary>
        /// ��ʼ����<see cref="TemplateTextElement"/>��
        /// </summary>
        /// <param name="source">Դ���롣</param>
        /// <param name="position">λ�á�</param>
        internal TemplateTextElement(string source, int position) : base(position, TemplateElementType.Text)
        {
            _source = source;
        }

        /// <summary>���ر�ʾ��ǰ������ַ�����</summary>
        /// <returns>��ʾ��ǰ������ַ�����</returns>
        public override string ToString() => _source;

        /// <summary>
        /// ���ɽű���
        /// </summary>
        /// <param name="executor">�ű��﷨��������</param>
        /// <returns>�������ɺ�Ľű���</returns>
        public override string ToJsString(ITemplateExecutor executor)
        {
            return $"html+='{_source?.Replace("'", "\'")}';";
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
            return _source;
        }
    }
}