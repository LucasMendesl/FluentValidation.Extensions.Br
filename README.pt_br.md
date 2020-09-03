
# FluentValidation.Extensions.Br
> Uma extensão da biblioteca FluentValidation com um conjunto de validações PT-BR

[![Nuget](http://img.shields.io/nuget/v/Extensions.FluentValidation.Br.svg?maxAge=10800)](https://www.nuget.org/packages/Extensions.FluentValidation.Br/)

[🇺🇸 Trocar para a versão em inglês](./README.md)

## Objetivo Principal
Essa biblioteca tem como objetivo principal criar um conjunto de validações de atributos brasileiros para o pacote [FluentValidation](https://github.com/JeremySkinner/FluentValidation)

## Descrição
Essa biblioteca foi desenvolvida para fornecer um conjunto de validações para atributos brasileiros como CPF, CNPJ etc.

## Instalação 
Instalando com Package Manager

**Nuget**

```
PM > Install-Package Extensions.FluentValidation.Br
```

Instalado com .NET CLI

**.NET CLI**

```
dotnet add package Extensions.FluentValidation.Br
```

## Como usar ?
```csharp
public class Person
{
    public string Name { get; set; }
    public string CPF  { get; set; }
    public string CNPJ { get; set; }
}

public class PersonValidator : AbstractValidator<Person>
{
  public PersonValidator ()
  {
      RuleFor(employee => employee.Name).NotNull();
      RuleFor(employee => employee.CPF).IsValidCPF();
      RuleFor(employee => employee.CNPJ).IsValidCNPJ();
  }
}
```

## Executando os testes
```
dotnet test
```

## Contribuindo
Contribuições via Pull Request são muito bem vindas :-)

## Licença
MIT © [Lucas Mendes Loureiro](http://github.com/lucasmendesl)
