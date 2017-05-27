using System;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// ģ��Ԫ�ػ��ࡣ
    /// </summary>
    public abstract class TemplateElementBase
    {
        /// <summary>
        /// ��ʼ����<see cref="TemplateElementBase"/>��
        /// </summary>
        /// <param name="position">λ�á�</param>
        /// <param name="type">���͡�</param>
        protected TemplateElementBase(int position, TemplateElementType type)
        {
            Position = position;
            Type = type;
        }

        /// <summary>
        /// ���ĵ��е�λ�á�
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// ���͡�
        /// </summary>
        public TemplateElementType Type { get; }

        /// <summary>
        /// �����ڵ㡣
        /// </summary>
        public TemplateElement Parent { get; internal set; }

        private TemplateDocument _document;
        /// <summary>
        /// �ĵ��ڵ㡣
        /// </summary>
        public TemplateDocument Document
        {
            get
            {
                if (_document == null)
                {
                    var element = this;
                    while (element.Type != TemplateElementType.Document)
                    {
                        element = element.Parent;
                    }
                    _document = element as TemplateDocument;
                }
                return _document;
            }
        }

        /// <summary>
        /// ���ɽű���
        /// </summary>
        /// <param name="executor">�ű��﷨��������</param>
        /// <returns>�������ɺ�Ľű���</returns>
        public abstract string ToJsString(ITemplateExecutor executor);

        /// <summary>
        /// ����HTML���롣
        /// </summary>
        /// <param name="executor">�﷨��������</param>
        /// <param name="instance">��ǰʵ����</param>
        /// <param name="func">��ȡʵ������ֵ��</param>
        /// <returns>����HTML���롣</returns>
        public abstract string ToHtmlString(ITemplateExecutor executor, object instance, Func<object, string, object> func);
    }
}