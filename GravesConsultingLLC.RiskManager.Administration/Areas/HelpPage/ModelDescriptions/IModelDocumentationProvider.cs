using System;
using System.Reflection;

namespace GravesConsultingLLC.RiskManager.Administration.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}