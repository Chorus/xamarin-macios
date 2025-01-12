using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.Macios.Generator.Context;

class ClassBindingContext : SymbolBindingContext {
	public string RegisterName { get; init; }

	public ClassBindingContext (RootBindingContext context, SemanticModel semanticModel,
		INamedTypeSymbol symbol)
		: base (context, semanticModel, symbol)
	{
		RegisterName =
			symbol.Name; //TODO: placeholder -> should this be extracted from the BindingTypeAttribute
	}
}
