using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// Children��䣨�ݹ飩��
    /// </summary>
    public class ChildrenTemplateSyntaxExecutor : ITemplateSyntaxExecutor
    {
        /// <summary>
        /// �ؼ��ʡ�
        /// </summary>
        public string Keyword => "children";

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
            return null;
        }

        /// <summary>
        /// ͨ����ǰ����ִ���﷨���õ����ַ�����
        /// </summary>
        /// <param name="element">����ʵ������</param>
        /// <param name="executor">�������ӿڡ�</param>
        /// <returns>���ؽű�����</returns>
        public string Begin(TemplateSyntaxElement element, ITemplateExecutor executor)
        {
            if (element.IsSelfClosed || !element.Any())
                return "if(Array.isArray($model)){$model.forEach(function(item){appender(item);});}";
            var sb = new StringBuilder();
            sb.Append("function children($model){");
            foreach (var node in element)
            {
                sb.Append(node.ToJsString(executor));
            }
            sb.Append("};");
            sb.Append("if(Array.isArray($model)){$model.forEach(function(item){children(item);});}");
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
            var sb = new StringBuilder();
            if (instance is IEnumerable models)
            {
                if (element.IsSelfClosed || !element.Any())
                {
                    foreach (var model in models)
                    {
                        sb.Append(element.Document.ToHtmlString(executor, model, func));
                    }
                }
                else
                {
                    foreach (var model in models)
                    {
                        sb.Append(element.ToHtmlString(executor, model, func));
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// ͨ����ǰ����ִ���﷨���õ����ַ�����
        /// </summary>
        /// <param name="element">����ʵ������</param>
        /// <param name="executor">�������ӿڡ�</param>
        /// <returns>���ؽű�����</returns>
        public string End(TemplateSyntaxElement element, ITemplateExecutor executor)
        {
            return null;
        }
    }
}