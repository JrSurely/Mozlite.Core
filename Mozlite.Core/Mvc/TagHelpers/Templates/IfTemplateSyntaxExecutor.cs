using System;
using System.Text;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// IF��䡣
    /// </summary>
    public class IfTemplateSyntaxExecutor : ITemplateSyntaxExecutor
    {
        /// <summary>
        /// �ؼ��ʡ�
        /// </summary>
        public string Keyword => "if";

        /// <summary>
        /// ͨ����ǰ����ִ���﷨���õ����ַ�����
        /// </summary>
        /// <param name="element">����ʵ������</param>
        /// <param name="executor">�������ӿڡ�</param>
        /// <param name="instance">��ǰʵ������</param>
        /// <param name="func">��ȡ��ǰ��������ֵ�ķ�����</param>
        /// <returns>���ؽű�����</returns>
        public string End(TemplateSyntaxElement element, ITemplateExecutor executor, object instance, Func<object, string, object> func)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ͨ����ǰ����ִ���﷨���õ����ַ�����
        /// </summary>
        /// <param name="element">����ʵ������</param>
        /// <param name="executor">�������ӿڡ�</param>
        /// <returns>���ؽű�����</returns>
        public string Begin(TemplateSyntaxElement element, ITemplateExecutor executor)
        {
            var sb = new StringBuilder();
            sb.Append("if(").Append(element.Args).Append("){");
            return sb.ToString();
        }

        /// <summary>
        /// ͨ����ǰ����ִ���﷨���õ����ַ�����
        /// </summary>
        /// <param name="element">����ʵ������</param>
        /// <param name="executor">�������ӿڡ�</param>
        /// <param name="instance">��ǰʵ������</param>
        /// <param name="func">��ȡ��ǰ��������ֵ�ķ�����</param>
        /// <returns>���ؽű�����</returns>
        public string Begin(TemplateSyntaxElement element, ITemplateExecutor executor, object instance, Func<object, string, object> func)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ͨ����ǰ����ִ���﷨���õ����ַ�����
        /// </summary>
        /// <param name="element">����ʵ������</param>
        /// <param name="executor">�������ӿڡ�</param>
        /// <returns>���ؽű�����</returns>
        public string End(TemplateSyntaxElement element, ITemplateExecutor executor)
        {
            return "}";
        }
    }
}