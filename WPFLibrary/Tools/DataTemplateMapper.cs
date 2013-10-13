using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using WPFLibrary.InternalBaseClasses;

namespace WPFLibrary.Tools
{
    internal class DataTemplateMapper
    {
        private DataTemplate viewModelMapDataTemplate(Type view, Type viewModel)
        {
            const string xamlTemplate = "<DataTemplate DataType=\"{{x:Type vm:{0}}}\"><v:{1} /></DataTemplate>";
            var xaml = String.Format(xamlTemplate, viewModel.Name, view.Name);

            var context = new ParserContext
            {
                XamlTypeMapper = new XamlTypeMapper(new string[0])
            };

            context.XamlTypeMapper.AddMappingProcessingInstruction("vm", viewModel.Namespace, viewModel.Assembly.FullName);
            context.XamlTypeMapper.AddMappingProcessingInstruction("v", view.Namespace, view.Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("vm", "vm");
            context.XmlnsDictionary.Add("v", "v");

            var template = (DataTemplate)XamlReader.Parse(xaml, context);
            return template;
        }

        public void MapViewModels(Assembly assembly)
        {
            var viewModels = assembly.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof (ViewModel)))
                .ToList();

            foreach (var viewModel in viewModels)
            {
                var viewType = viewModel.BaseType.GenericTypeArguments.Single(x => x.IsSubclassOf(typeof (ContentControl)));
                var dataTemplate = viewModelMapDataTemplate(viewType, viewModel);
                Application.Current.Resources.Add(dataTemplate.DataTemplateKey,dataTemplate);
            }
        }
    }
}
