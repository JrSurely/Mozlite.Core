using System.Collections.Generic;
using Mozlite.Core;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// ����ִ�нӿڣ�һ�����"jmoz:"��ͷ�����ԡ�
    /// </summary>
    public interface ITemplateHtmlAttributeExecutor : IServices
    {
        /// <summary>
        /// �ؼ��ʡ�
        /// </summary>
        string Keyword { get; }

        /// <summary>
        /// ͨ����ǰ����ִ���������õ����ַ�����
        /// </summary>
        /// <param name="attribute">����ʵ������</param>
        /// <param name="result">��ǰԭ�е������б�</param>
        /// <returns>���ؽű�����</returns>
        string Execute(TemplateHtmlCodeAttribute attribute, Dictionary<string, string> result);

        /// <summary>
        /// ͨ����ǰ����ִ���������õ����ַ�����
        /// </summary>
        /// <param name="attribute">����ʵ������</param>
        /// <param name="result">��ǰԭ�е������б�</param>
        /// <param name="instance">��ǰʵ������</param>
        void Execute(TemplateHtmlCodeAttribute attribute, Dictionary<string, string> result, object instance);
    }
}