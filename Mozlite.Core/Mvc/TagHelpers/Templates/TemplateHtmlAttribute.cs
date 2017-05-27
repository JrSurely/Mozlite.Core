namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// HTML���ԡ�
    /// </summary>
    public class TemplateHtmlAttribute
    {
        /// <summary>
        /// �������ơ�
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// ����ֵ��
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// ����Ԫ�ء�
        /// </summary>
        public TemplateHtmlElement Element { get; set; }

        /// <summary>
        /// ���͡�
        /// </summary>
        public TemplateHtmlAttributeType Type { get; }

        /// <summary>
        /// ��ʼ����<see cref="TemplateHtmlAttribute"/>��
        /// </summary>
        /// <param name="name">�������ơ�</param>
        /// <param name="value">����ֵ��</param>
        /// <param name="type">���͡�</param>
        public TemplateHtmlAttribute(string name, string value,
            TemplateHtmlAttributeType type = TemplateHtmlAttributeType.Text)
        {
            Name = name;
            Value = value;
            Type = type;
        }

        /// <summary>
        /// ��ʼ����<see cref="TemplateHtmlAttribute"/>��
        /// </summary>
        /// <param name="type">���͡�</param>
        internal TemplateHtmlAttribute(TemplateHtmlAttributeType type)
        {
            Type = type;
        }

        /// <summary>���ر�ʾ��ǰ������ַ�����</summary>
        /// <returns>��ʾ��ǰ������ַ�����</returns>
        public override string ToString()
        {
            return $"{Name}=\"{Value}\"";
        }
    }
}