using System.Text;

namespace Mozlite.Mvc.TagHelpers.Ajax
{
    /// <summary>
    /// HTML�����ࡣ
    /// </summary>
    public class ScriptWriter
    {
        private readonly StringBuilder _sb;
        /// <summary>
        /// ��ʼ����<see cref="ScriptWriter"/>��
        /// </summary>
        public ScriptWriter()
        {
            _sb = new StringBuilder();
        }

        /// <summary>
        /// д����롣
        /// </summary>
        /// <param name="code">���롣</param>
        /// <returns>���ص�ǰ������ʵ����</returns>
        public ScriptWriter Write(string code)
        {
            _sb.Append(code);
            return this;
        }

        /// <summary>
        /// д��HTML���롣
        /// </summary>
        /// <param name="code">HTML���롣</param>
        /// <returns>���ص�ǰ������ʵ����</returns>
        public ScriptWriter Html(string code)
        {
            code = code?.Trim().Replace("`", "\\`");
            return Write("_s.push(`")
                .Write(code)
                .Write("`);");
        }

        /// <summary>
        /// д����������
        /// </summary>
        /// <param name="code">�ַ�����</param>
        /// <returns>���ص�ǰ������ʵ����</returns>
        public ScriptWriter Code(string code)
        {
            return Write("_s.push(").Write(code).Write(");");
        }

        /// <summary>
        /// д���ַ�����
        /// </summary>
        /// <param name="text">�ַ�����</param>
        /// <returns>���ص�ǰ������ʵ����</returns>
        public ScriptWriter Quote(string text)
        {
            return Write("'").Write(text?.Replace("'", "\\'")).Write("'");
        }

        /// <summary>���ر�ʾ��ǰ������ַ�����</summary>
        /// <returns>��ʾ��ǰ������ַ�����</returns>
        public override string ToString()
        {
            return _sb.ToString();
        }
    }
}