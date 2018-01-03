using System;
using System.Collections.Generic;
using System.Text;
using Markdig;
using ScriptEngine.Machine.Contexts;

namespace MdRenderProxy
{
    [ContextClass("ПарсерРазметкиMD","MarkdownParser")]
    public class MarkdownRender : AutoContext<MarkdownRender>
    {
        private MarkdownPipeline _parser;

        [ContextProperty("ВключитьРасширения", "EnableExtensions")]
        public bool EnableExtensions { get; set; }

        [ContextMethod("ПодготовитьПарсер", "PrepareParser")]
        public void PrepareParser()
        {
            var builder = new MarkdownPipelineBuilder();
            if (EnableExtensions)
                builder.UseAdvancedExtensions();

            _parser = builder.Build();
        }

        [ContextMethod("СоздатьHTML","CreateHTML")]
        public string CreateHtml(string markdownText)
        {
            if(_parser == null)
                PrepareParser();

            var result = Markdown.ToHtml(markdownText, _parser);

            return result;
        }

        [ScriptConstructor]
        public static MarkdownRender Constructor()
        {
            return new MarkdownRender() { EnableExtensions = true };
        }
    }
}
