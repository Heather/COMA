```nemerle
mutable builder = using(reader2 = StreamReader(fullName))
        (StringBuilder(reader2.ReadToEnd())).Replace(
        ".corflags 0x00000001", ".corflags 0x00000002")
mutable num = 0
for (mutable i = builder.ToString().IndexOf("System.Reflection.ObfuscationAttribute", 0);
        i != -1;
        i = builder.ToString().IndexOf("System.Reflection.ObfuscationAttribute", i))
    num++
    i = builder.ToString().IndexOf("// llExport\r\n", i) + 13
    builder = builder.Insert(i, "    .export[" + num + "]\r\n")
def bytes = Encoding.UTF8.GetBytes(builder.ToString())
using (def stream = FileStream(fullName, FileMode.Create))
    stream.WriteByte(0xef)
    stream.WriteByte(0xbb)
    stream.WriteByte(0xbf)
    stream.Write(bytes, 0, bytes.Length)
def process2 = Process() <- { 
    StartInfo = ProcessStartInfo(asm
        , " /DLL /OPTIMIZE /REsrc=\"" 
            + fullName.Substring(0, fullName.LastIndexOf(".")) 
            + ".res\" \"" 
            + fullName + "\"") }
```