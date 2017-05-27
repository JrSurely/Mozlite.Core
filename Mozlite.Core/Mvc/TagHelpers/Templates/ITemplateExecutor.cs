using System;
using System.Collections.Generic;
using Mozlite.Core;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// ģ���﷨�������ӿڡ�
    /// </summary>
    public interface ITemplateExecutor : ISingletonService
    {
        /// <summary>
        /// ���Ի�ȡ�﷨��������
        /// </summary>
        /// <param name="element">Ԫ��ʵ������</param>
        /// <param name="executor">���صĽ�������</param>
        /// <returns>���ػ�ȡ�����</returns>
        bool TryGetExecutor(TemplateSyntaxElement element, out ITemplateSyntaxExecutor executor);

        /// <summary>
        /// ͨ����ǰ����ִ���������õ����ַ�����
        /// </summary>
        /// <param name="attributes">����ʵ�������б�</param>
        /// <param name="result">��ǰԭ�е������б�</param>
        /// <returns>���ؽű�����</returns>
        string Execute(IEnumerable<TemplateHtmlCodeAttribute> attributes, Dictionary<string, string> result);

        /// <summary>
        /// ͨ����ǰ����ִ���������õ����ַ�����
        /// </summary>
        /// <param name="attributes">����ʵ�������б�</param>
        /// <param name="result">��ǰԭ�е������б�</param>
        /// <param name="instance">��ǰʵ������</param>
        /// <param name="func">��ȡ��ǰ��������ֵ�ķ�����</param>
        void Execute(IEnumerable<TemplateHtmlCodeAttribute> attributes, Dictionary<string, string> result, object instance, Func<object, string, object> func);
    }
}