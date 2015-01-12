#Turkish Deasciifier for .NET
Turkish Deasciifier is a .net library which converts Turkish text written with ASCII-only sentences into proper Turkish text with Turkish-specific accented letters.

(Turkish deasciifier ile Türkçe karakterler (ş, ı, ö, ç, ğ, ü) kullanmadan yazılmış yazıları doğru Türkçe karakter karşılıkları ile düzeltebilirsiniz.)

For instance a Turkish sentence containing only ASCII characters like:

Hadi bir masal uyduralim, icinde mutlu, doygun, telassiz durdugumuz.
will be converted to a sentence containing proper Turkish accented characters:

Hadi bir masal uyduralım, içinde mutlu, doygun, telaşsız durduğumuz.
Credits

This library is adapted from Ahmet Alp Balan's java library and it is adapted from Emre Sevinç's Turkish Deasciifier for Python which was influenced by Deniz Yüret's Emacs Turkish Mode implementation which was inspired by Gökhan Tür's Turkish Text Deasciifier.

This project is not actively maintained.

Example usage

Deasciifier d = new Deasciifier();
d.setAsciiString("Hadi bir masal uyduralim, icinde mutlu, doygun, telassiz durdugumuz.");
System.out.println(d.convertToTurkish());
That simple.
