using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mozlite.Data.Metadata;
using Newtonsoft.Json;

namespace Mozlite.Mvc.TagHelpers.Templates
{
    /// <summary>
    /// ģ���ǩ��
    /// </summary>
    [HtmlTargetElement("*", Attributes = "[jmoz-template=json]")]
    [HtmlTargetElement("*", Attributes = "[jmoz-template=data]")]
    public class TemplateTagHelper : TagHelper
    {
        private readonly ITemplateExecutor _executor;

        public TemplateTagHelper(ITemplateExecutor executor, IModel model)
        {
            _executor = executor;
            _model = model;
        }

        private readonly IModel _model;
        private IEntityType _entityType;

        /// <summary>
        /// ��ȡ��ǰ������<paramref name="propertyName"/>��ֵ��
        /// </summary>
        /// <param name="model">ģ��ʵ����</param>
        /// <param name="propertyName">�������ơ�</param>
        /// <returns>���ص�ǰ���Ե�ֵ��</returns>
        protected object GetValue(object model, string propertyName)
        {
            if (propertyName == "$site")
                return (model as IEnumerable).OfType<object>().Count();
            _entityType = _entityType ?? _model.GetEntity(model.GetType());
            return _entityType?.FindProperty(propertyName)?.Get(model);
        }

        /// <summary>
        /// �����͡�
        /// </summary>
        [HtmlAttributeName("jmoz-template")]
        public string Binder { get; set; }

        /// <summary>
        /// ���ݡ�
        /// </summary>
        [HtmlAttributeName("jmoz-models")]
        public object Models { get; set; }

        /// <summary>
        /// Զ�����ݡ�
        /// </summary>
        [HtmlAttributeName("jmoz-remote")]
        public string RemoteUrl { get; set; }

        /// <summary>
        /// �ص�������
        /// </summary>
        [HtmlAttributeName("jmoz-callback")]
        public string Callback { get; set; }

        /// <summary>
        /// ���͵����ݡ�
        /// </summary>
        [HtmlAttributeName("jmoz-data", DictionaryAttributePrefix = "jmoz-data-")]
        public IDictionary<string, string> Data { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        
        /// <summary>
        /// ��һ�λ�ȡ���ݵ�ʱ�������룩��
        /// </summary>
        [HtmlAttributeName("jmoz-interval")]
        public int Interval { get; set; }

        /// <summary>
        /// ���ֽű���
        /// </summary>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var template = (await output.GetChildContentAsync()).GetContent();
            if (string.IsNullOrWhiteSpace(template) || (Models == null && RemoteUrl == null))
            {
                output.SuppressOutput();
                return;
            }

            var document = new TemplateDocument(template);
            if (Binder == "json")
            {
                var id = context.AllAttributes["id"]?.Value;
                if (id == null)
                {
                    id = "jmoz_" + context.UniqueId;
                    output.Attributes.SetAttribute("id", id);
                }

                output.Content.AppendHtml("<script>");
                output.Content.AppendHtml("$(function(){");
                output.Content.AppendHtml($"var $this = $('#{id}');");
                Render(output, document);
                output.Content.AppendHtml(document.Scripts);
                output.Content.AppendHtml("});");
                output.Content.AppendHtml("</script>");
            }
            else
            {
                if (Models is IEnumerable instances)
                {
                    foreach (var instance in instances)
                    {
                        output.Content.AppendHtml(document.ToHtmlString(_executor, instance, GetValue));
                    }
                }
                else
                {
                    output.Content.AppendHtml(document.ToHtmlString(_executor, Models, GetValue));
                }
                if (!string.IsNullOrWhiteSpace(document.Scripts))
                    output.Content.AppendHtml("<script>").AppendHtml(document.Scripts).Append("</script>");
            }
        }

        private void Render(TagHelperOutput output, TemplateDocument document)
        {
            Func(output, "render", "data", c =>
            {
                c.AppendHtml("var html = '';");
                Func(output, "appender", "$model", ac => ac.AppendHtml(document.ToJsString(_executor)));
                c.AppendHtml("appender(data);");
                c.AppendHtml("$this.html(html);");
            });
            if (Models == null || Interval > 0)
            {
                Func(output, "ajax", "url", c =>
                {
                    c.AppendHtml("$.ajax({url:url+'&_'+(+new Date())")
                        .AppendHtml(", dataType:'JSON', type:'GET', success:function(data){")
                        .AppendHtml("render(data);");

                    if (Callback != null)
                        c.AppendHtml(Callback).AppendHtml("(data);");

                    if (Interval > 0)
                    {//��ʱ��
                        c.AppendHtml("setTimeout(function(){ajax(url);}")
                            .AppendHtml(",")
                            .AppendHtml((Interval * 1000).ToString())
                            .AppendHtml(");");
                    }
                    c.AppendHtml("}, error:function(r){$this.html(r.responseText);");
                    if (Interval > 0)
                    {//��ʱ��
                        c.AppendHtml("setTimeout(function(){ajax(url);}")
                            .AppendHtml(",")
                            .AppendHtml((Interval * 1000).ToString())
                            .AppendHtml(");");
                    }
                    c.AppendHtml("}});");
                });
            }

            switch (Models)
            {
                case string jscode when jscode.Trim().Length > 0:
                    output.Content.AppendHtml("render(");
                    output.Content.AppendHtml(jscode);
                    output.Content.AppendHtml(");");
                    break;
                default:
                    output.Content.AppendHtml("render(");
                    output.Content.AppendHtml(JsonConvert.SerializeObject(Models));
                    output.Content.AppendHtml(");");
                    break;
                case null:
                    output.Content.AppendHtml("ajax(");
                    var url = RemoteUrl;
                    if (url.IndexOf('?') == -1)
                        url += '?';
                    else
                        url += '&';
                    url += string.Join("&", Data.Select(x => $"{x.Key}={x.Value}"));
                    output.Content.AppendHtml("'").AppendHtml(url).AppendHtml("'");
                    output.Content.AppendHtml(");");
                    break;
            }
        }

        private void Func(TagHelperOutput output, string name, string args, Action<TagHelperContent> action)
        {
            output.Content.AppendHtml("function ");
            output.Content.AppendHtml(name);
            output.Content.AppendHtml("(");
            output.Content.AppendHtml(args);
            output.Content.AppendHtml(")");
            output.Content.AppendHtml("{");
            action(output.Content);
            output.Content.AppendHtml("};");
        }
    }
}