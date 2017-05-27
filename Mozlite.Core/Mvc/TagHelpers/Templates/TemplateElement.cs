using System.Collections;
using System.Collections.Generic;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// �����ӽڵ�Ľڵ���ࡣ
    /// </summary>
    public abstract class TemplateElement : TemplateElementBase, IEnumerable<TemplateElementBase>
    {
        private readonly List<TemplateElementBase> _elements = new List<TemplateElementBase>();
        /// <summary>
        /// ��ʼ����<see cref="TemplateElement"/>��
        /// </summary>
        /// <param name="position">λ�á�</param>
        /// <param name="type">���͡�</param>
        protected TemplateElement(int position, TemplateElementType type) : base(position, type)
        {
        }

        /// <summary>
        /// ����ӽڵ㡣
        /// </summary>
        /// <param name="element">�ڵ�ʵ����</param>
        public void Add(TemplateElementBase element)
        {
            element.Parent = this;
            _elements.Add(element);
        }

        /// <summary>
        /// ����ӽڵ㡣
        /// </summary>
        /// <param name="elements">�ڵ�ʵ���б�</param>
        public void AddRange(IEnumerable<TemplateElementBase> elements)
        {
            foreach (var element in elements)
            {
                Add(element);
            }
        }

        /// <summary>����һ��ѭ�����ʼ��ϵ�ö������</summary>
        /// <returns>����ѭ�����ʼ��ϵ�ö������</returns>
        public IEnumerator<TemplateElementBase> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        /// <summary>����ѭ�����ʼ��ϵ�ö������</summary>
        /// <returns>������ѭ�����ʼ��ϵ� <see cref="T:System.Collections.IEnumerator" /> ����</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>���ر�ʾ��ǰ������ַ�����</summary>
        /// <returns>��ʾ��ǰ������ַ�����</returns>
        public override string ToString()
        {
            return string.Join("\r\n", this);
        }
    }
}