namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// ģ��ڵ����͡�
    /// </summary>
    public enum TemplateElementType
    {
        /// <summary>
        /// �ĵ�����߽ڵ㡣
        /// </summary>
        Document,

        /// <summary>
        /// �ı���
        /// </summary>
        Text,

        /// <summary>
        /// HTML��ǩ��
        /// </summary>
        Html,

        /// <summary>
        /// ���롰{{code}}����
        /// </summary>
        Code,

        /// <summary>
        /// ��䣨�н������Ĵ���飩��{{code/}}����"{{code}}text{{/code}}"��
        /// </summary>
        Syntax,
    }
}