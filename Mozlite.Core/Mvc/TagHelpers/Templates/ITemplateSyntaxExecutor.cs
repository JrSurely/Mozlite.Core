using System;
using Mozlite.Core;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// ���ִ�н����ӿڡ�
    /// </summary>
    public interface ITemplateSyntaxExecutor : IServices
    {
        /// <summary>
        /// �ؼ��ʡ�
        /// </summary>
        string Keyword { get; }

        /// <summary>
        /// ͨ����ǰ����ִ���﷨���õ����ַ�����
        /// </summary>
        /// <param name="element">����ʵ������</param>
        /// <param name="executor">�������ӿڡ�</param>
        /// <param name="instance">��ǰʵ������</param>
        /// <param name="func">��ȡ��ǰ��������ֵ�ķ�����</param>
        /// <returns>���ؽű�����</returns>
        string End(TemplateSyntaxElement element, ITemplateExecutor executor, object instance, Func<object, string, object> func);

        /// <summary>
        /// ͨ����ǰ����ִ���﷨���õ����ַ�����
        /// </summary>
        /// <param name="element">����ʵ������</param>
        /// <param name="executor">�������ӿڡ�</param>
        /// <returns>���ؽű�����</returns>
        string Begin(TemplateSyntaxElement element, ITemplateExecutor executor);

        /// <summary>
        /// ͨ����ǰ����ִ���﷨���õ����ַ�����
        /// </summary>
        /// <param name="element">����ʵ������</param>
        /// <param name="executor">�������ӿڡ�</param>
        /// <param name="instance">��ǰʵ������</param>
        /// <param name="func">��ȡ��ǰ��������ֵ�ķ�����</param>
        /// <returns>���ؽű�����</returns>
        string Begin(TemplateSyntaxElement element, ITemplateExecutor executor, object instance, Func<object, string, object> func);

        /// <summary>
        /// ͨ����ǰ����ִ���﷨���õ����ַ�����
        /// </summary>
        /// <param name="element">����ʵ������</param>
        /// <param name="executor">�������ӿڡ�</param>
        /// <returns>���ؽű�����</returns>
        string End(TemplateSyntaxElement element, ITemplateExecutor executor);
    }
}