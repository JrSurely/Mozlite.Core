using System.Collections.Generic;
using System.Text;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// Ifset�ؼ��ʡ�
    /// </summary>
    /// <example>
    /// ��ʽ
    ///     jmoz:ifset-class="{{(name=null):'true-class':'false-class'}}"
    ///     jmoz:ifset-class="{{(name=null):'true-class'}}"
    /// </example>
    public class IfsetTemplateHtmlAttributeExecutor : ITemplateHtmlAttributeExecutor
    {
        /// <summary>
        /// �ؼ��ʡ�
        /// </summary>
        public string Keyword => "ifset";

        /// <summary>
        /// ͨ����ǰ����ִ���������õ����ַ�����
        /// </summary>
        /// <param name="attribute">����ʵ������</param>
        /// <param name="result">��ǰԭ�е������б�</param>
        /// <returns>���ؽű�����</returns>
        public string Execute(TemplateHtmlCodeAttribute attribute, Dictionary<string, string> result)
        {
            var sb = new StringBuilder();
            if (result.TryGetValue(attribute.AttributeName, out var value))
            {
                result.Remove(attribute.AttributeName);
            }
            sb.Append("if(")
                .Append(attribute.Value)
                .Append("){")
                .SetJsAttribute(attribute.AttributeName, attribute[0]);
            if (attribute.Count == 2)
                sb.Append("}else{").SetJsAttribute(attribute.AttributeName, attribute[1]);
            else if (!string.IsNullOrWhiteSpace(value))
                sb.Append("}else{").SetJsAttribute(attribute.AttributeName, value);
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// ͨ����ǰ����ִ���������õ����ַ�����
        /// </summary>
        /// <param name="attribute">����ʵ������</param>
        /// <param name="result">��ǰԭ�е������б�</param>
        /// <param name="instance">��ǰʵ������</param>
        public void Execute(TemplateHtmlCodeAttribute attribute, Dictionary<string, string> result, object instance)
        {
            var value = attribute.Value?.Trim();
            if (!string.IsNullOrWhiteSpace(value))
            {
                var isTrue = (bool)TemplateExpression.Execute(value, instance);
                if (isTrue)
                    result[attribute.AttributeName] = attribute[0].Trim('\'');
                else if (attribute.Count == 2)
                    result[attribute.AttributeName] = attribute[1].Trim('\'');
            }
        }
    }
}