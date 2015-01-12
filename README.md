#Turkish Deasciifier for .NET
Turkish Deasciifier is a .net library which converts Turkish text written with ASCII-only sentences into proper Turkish text with Turkish-specific accented letters.

(Turkish deasciifier ile Türkçe karakterler (ş, ı, ö, ç, ğ, ü) kullanmadan yazılmış yazıları doğru Türkçe karakter karşılıkları ile düzeltebilirsiniz.)

For instance a Turkish sentence containing only ASCII characters like:

>  Hadi bir masal uyduralim, icinde mutlu, doygun, telassiz durdugumuz.

will be converted to a sentence containing proper Turkish accented characters:

> Hadi bir masal uyduralım, içinde mutlu, doygun, telaşsız durduğumuz.

### Credits

This library is adapted from [Ahmet Alp Balkan's](https://github.com/ahmetalpbalkan/turkish-deasciifier-java) java library and it is adapted from Emre Sevinç's [Turkish Deasciifier](http://ileriseviye.org/blog/?p=327) for Python which was influenced by Deniz Yüret's [Emacs Turkish Mode](http://denizyuret.blogspot.com/2006/11/emacs-turkish-mode.html) implementation which was inspired by Gökhan Tür's [Turkish Text Deasciifier](http://www.hlst.sabanciuniv.edu/TL/deascii.html).


This project is not actively maintained.

### Example usage

```csharp
Deasciifier d = new Deasciifier();
d.SetAsciiString("Hadi bir masal uyduralim, icinde mutlu, doygun, telassiz durdugumuz.");
System.out.println(d.ConvertToTurkish());
```
