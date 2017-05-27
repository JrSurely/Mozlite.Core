using System;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// Foreach��䡣
    /// </summary>
    public class ForeachTemplateSyntaxExecutor : ITemplateSyntaxExecutor
    {
        /// <summary>
        /// �ؼ��ʡ�
        /// </summary>
        public string Keyword => "foreach";

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
            var items = element.Args.Trim().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length == 3 && items[1] == "in")
                return $"{items[2]}.forEach(function({items[0]},index){{";
            throw new Exception("�﷨���󣬱���Ϊ{{foreach xx in xxs}}!");
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
            return "});";
        }
    }
}