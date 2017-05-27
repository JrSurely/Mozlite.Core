using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// Html�ڵ㡣
    /// </summary>
    public class TemplateHtmlElement : TemplateElement
    {
        private readonly Dictionary<string, TemplateHtmlCodeAttribute> _attributes = new Dictionary<string, TemplateHtmlCodeAttribute>();
        private readonly Dictionary<string, TemplateHtmlAttribute> _basics = new Dictionary<string, TemplateHtmlAttribute>();
        /// <summary>
        /// ��ʼ����<see cref="TemplateHtmlElement"/>��
        /// </summary>
        /// <param name="tagName">��ǩ���ơ�</param>
        /// <param name="position">λ�á�</param>
        protected internal TemplateHtmlElement(string tagName, int position) : base(position, TemplateElementType.Html)
        {
            TagName = tagName;
        }

        /// <summary>
        /// ��ȡ��ǰ���Ƶ�����ʵ����
        /// </summary>
        /// <param name="name">�������ơ�</param>
        /// <returns>��������ʵ������</returns>
        public TemplateHtmlAttribute this[string name]
        {
            get
            {
                if (_attributes.TryGetValue(name, out var value))
                    return value;
                if (_basics.TryGetValue(name, out var basic))
                    return basic;
                return null;
            }
            set
            {
                var attribute = value;
                if (attribute != null)
                    attribute.Element = this;
                if (attribute is TemplateHtmlCodeAttribute code)
                    _attributes[name] = code;
                else
                    _basics[name] = attribute;
            }
        }

        /// <summary>
        /// ������ơ�
        /// </summary>
        public string TagName { get; }

        /// <summary>
        /// �Ƿ�Ϊ�ԱպϽڵ㡣
        /// </summary>
        public bool IsSelfClosed { get; set; }

        /// <summary>���ر�ʾ��ǰ������ַ�����</summary>
        /// <returns>��ʾ��ǰ������ַ�����</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("<").Append(TagName);
            if (_basics.Count > 0)
                sb.Append(" ").Append(string.Join(" ", _basics.Values.Where(x => !string.IsNullOrWhiteSpace(x.Value)).Select(x => $"{x.Name}=\"{x.Value.Replace("'", "\\'")}\"")));
            if (IsSelfClosed)
            {
                sb.AppendLine("/>");
                return sb.ToString();
            }
            sb.AppendLine(">");
            sb.Append(base.ToString());
            sb.AppendFormat("</{0}>", TagName).AppendLine();
            return sb.ToString();
        }

        /// <summary>
        /// ���ɽű���
        /// </summary>
        /// <param name="executor">�ű��﷨��������</param>
        /// <returns>�������ɺ�Ľű���</returns>
        public override string ToJsString(ITemplateExecutor executor)
        {
            var sb = new StringBuilder();
            AppendStartJsString(sb, executor);
            if (IsSelfClosed) return sb.ToString();
            foreach (var node in this)
            {
                sb.Append(node.ToJsString(executor));
            }
            sb.Append("html+='").Append("</").Append(TagName).Append(">';");
            return sb.ToString();
        }

        /// <summary>
        /// ����HTML���롣
        /// </summary>
        /// <param name="executor">�﷨��������</param>
        /// <param name="instance">��ǰʵ����</param>
        /// <param name="func">��ȡʵ������ֵ��</param>
        /// <returns>����HTML���롣</returns>
        public override string ToHtmlString(ITemplateExecutor executor, object instance, Func<object, string, object> func)
        {
            var sb = new StringBuilder();
            sb.Append("<").Append(TagName);
            if (_basics.Count > 0 || _attributes.Count > 0)
            {
                var result = _basics.ToDictionary(x => x.Key, x => _regex.Replace(x.Value.Value, match => func(instance, match.Groups[1].Value.Trim())?.ToString()));
                executor.Execute(_attributes.Values, result, instance, func);
                foreach (var basic in result)
                {
                    sb.Append(" ")
                        .Append(basic.Key)
                        .Append("=\"")
                        .Append(basic.Value)
                        .Append("\"");
                }
            }
            if (IsSelfClosed)
            {
                sb.Append("/>");
                return sb.ToString();
            }
            sb.Append(">");
            foreach (var node in this)
            {
                sb.Append(node.ToHtmlString(executor, instance, func));
            }
            sb.Append("</").Append(TagName).Append(">");
            return sb.ToString();
        }


        private static readonly Regex _regex = new Regex("{{(.*?)}}", RegexOptions.Singleline);
        private void AppendStartJsString(StringBuilder sb, ITemplateExecutor executor)
        {
            sb.Append("html+='<").Append(TagName);
            if (_basics.Count > 0 || _attributes.Count > 0)
            {
                var basics = _basics.ToDictionary(x => x.Key, x => _regex.Replace(x.Value.Value, "'+$1+'"));
                var js = executor.Execute(_attributes.Values, basics);
                foreach (var basic in basics)
                {
                    sb.Append(" ")
                        .Append(basic.Key)
                        .Append("=\"")
                        .Append(basic.Value)
                        .Append("\"");
                }
                if (!string.IsNullOrWhiteSpace(js))
                {
                    sb.Append("';");
                    sb.Append(js);
                    sb.Append("html+='");
                }
            }
            if (IsSelfClosed)
                sb.Append("/");
            sb.Append(">';");
        }
    }
}