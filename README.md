
# FluentValidation.Extensions.Br
> An extension of the fluent validation with a set of Brazilian validations

[![Nuget](http://img.shields.io/nuget/v/Extensions.FluentValidation.Br.svg?maxAge=10800)](https://www.nuget.org/packages/Extensions.FluentValidation.Br/)

[🇧🇷 Switch to portuguese version](./README.pt_br.md)

## Main Goal
The prupose of this library is create a extension with brazilian´s validation to [FluentValidation](https://github.com/JeremySkinner/FluentValidation) package.

## Description
This library was designed to provide a set of brazilian´s attributes validation such as CPF, CNPJ, UF

## Install 
Install with Package Manager

**Nuget**

```
PM > Install-Package Extensions.FluentValidation.Br
```

Install with .NET CLI

**.NET CLI**

```
dotnet add package Extensions.FluentValidation.Br
```

## How to Use ?
```csharp
public class Person
{
    public string Name { get; set; }
    public string CPF  { get; set; }
    public string CNPJ { get; set; }
    public string UF { get; set; }
}

public class PersonValidator : AbstractValidator<Person>
{
  public PersonValidator ()
  {
      RuleFor(employee => employee.Name).NotNull();
      RuleFor(employee => employee.CPF).IsValidCPF();
      RuleFor(employee => employee.CNPJ).IsValidCNPJ();
      RuleFor(employee => employee.UF).IsValidUF();
  }
}
```

## Run Tests
```
dotnet test
```

## Contributing
Contributions via pull requests are welcome :-).

## License

MIT © [Lucas Mendes Loureiro](http://github.com/lucasmendesl)
