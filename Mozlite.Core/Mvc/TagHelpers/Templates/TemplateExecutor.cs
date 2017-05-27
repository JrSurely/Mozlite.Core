using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// �﷨����ִ�й���ʵ���ࡣ
    /// </summary>
    public class TemplateExecutor : ITemplateExecutor
    {
        private readonly ConcurrentDictionary<string, ITemplateSyntaxExecutor> _executors;
        private readonly ConcurrentDictionary<string, ITemplateHtmlAttributeExecutor> _attributes;
        public TemplateExecutor(IEnumerable<ITemplateSyntaxExecutor> executors, IEnumerable<ITemplateHtmlAttributeExecutor> attributes)
        {
            _executors = new ConcurrentDictionary<string, ITemplateSyntaxExecutor>(executors.ToDictionary(x => x.Keyword));
            _attributes = new ConcurrentDictionary<string, ITemplateHtmlAttributeExecutor>(attributes.ToDictionary(x => "jmoz:" + x.Keyword));
        }

        /// <summary>
        /// ���Ի�ȡ�﷨��������
        /// </summary>
        /// <param name="element">Ԫ��ʵ������</param>
        /// <param name="executor">���صĽ�������</param>
        /// <returns>���ػ�ȡ�����</returns>
        public bool TryGetExecutor(TemplateSyntaxElement element, out ITemplateSyntaxExecutor executor)
        {
            return _executors.TryGetValue(element.Keyword, out executor);
        }

        /// <summary>
        /// ͨ����ǰ����ִ���������õ����ַ�����
        /// </summary>
        /// <param name="attributes">����ʵ�������б�</param>
        /// <param name="result">��ǰԭ�е������б�</param>
        /// <returns>���ؽű�����</returns>
        public string Execute(IEnumerable<TemplateHtmlCodeAttribute> attributes, Dictionary<string, string> result)
        {
            var sb = new StringBuilder();
            foreach (var attribute in attributes)
            {
                if (_attributes.TryGetValue(attribute.Name, out var executor))
                {
                    sb.Append(executor.Execute(attribute, result));
                }
                else
                {
                    sb.Append("html+=' ")
                        .Append(attribute.Name)
                        .Append("=\"'+")
                        .Append(attribute.Value)
                        .Append("+'\"';");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// ͨ����ǰ����ִ���������õ����ַ�����
        /// </summary>
        /// <param name="attributes">����ʵ�������б�</param>
        /// <param name="result">��ǰԭ�е������б�</param>
        /// <param name="instance">��ǰʵ������</param>
        /// <param name="func">��ȡ��ǰ��������ֵ�ķ�����</param>
        public void Execute(IEnumerable<TemplateHtmlCodeAttribute> attributes, Dictionary<string, string> result, object instance, Func<object, string, object> func)
        {
            foreach (var attribute in attributes)
            {
                if (_attributes.TryGetValue(attribute.Name, out var executor))
                {
                    executor.Execute(attribute, result, instance);
                }
                else
                {
                    result[attribute.Name] = func(instance, attribute.Value)?.ToString();
                }
            }
        }
    }
}