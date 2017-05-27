using System.Collections.Generic;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// �������ԣ���jmoz:��ͷ�����ԡ�
    /// </summary>
    public class TemplateHtmlCodeAttribute : TemplateHtmlAttribute
    {
        /// <summary>
        /// ��Ӱ���HTML�������ơ�
        /// </summary>
        public string AttributeName { get; }

        private readonly List<string> _args = new List<string>();

        /// <summary>
        /// ��ʼ����<see cref="TemplateHtmlCodeAttribute"/>��
        /// </summary>
        /// <param name="name">�������ơ�</param>
        /// <param name="value">����ֵ��</param>
        public TemplateHtmlCodeAttribute(string name, string value)
            : base(TemplateHtmlAttributeType.Code)
        {
            var index = name.IndexOf('-');
            if (index == -1)
                Name = name;
            else
            {
                Name = name.Substring(0, index);
                AttributeName = name.Substring(index + 1);
            }
            value = value.Trim();
            var condition = new TemplateString(value);
            Value = condition.ReadUntil(':', '?');
            while (condition.CanRead)
            {
                _args.Add(condition.ReadUntil(':'));
            }
        }

        /// <summary>���ر�ʾ��ǰ������ַ�����</summary>
        /// <returns>��ʾ��ǰ������ַ�����</returns>
        public override string ToString()
        {
            if (AttributeName == null)
                return $"{Name}=\"{{{{{Value}}}}}\"";
            return $"{Name}-{AttributeName}=\"{{{{{Value}}}}}\"";
        }
        
        /// <summary>
        /// ����������
        /// </summary>
        public int Count => _args.Count;

        /// <summary>
        /// ��ȡ��ǰ�����Ĳ���ֵ��
        /// </summary>
        /// <param name="index">��ǰ����ֵ��</param>
        /// <returns>���ص�ǰ�����Ĳ���ֵ��</returns>
        public string this[int index]
        {
            get
            {
                if (index < Count) return _args[index];
                return null;
            }
        }
    }
}