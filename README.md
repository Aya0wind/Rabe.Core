# Rabe.Core
Rust Attribute-Based Encryption library [rabe](https://github.com/Fraunhofer-AISEC/rabe)'s C# binding , support multiple CP-ABE and KP-ABE encrypt and decrypt algorithms.  
Provide cipher and key json serialization and deserialization with System.Text.Json.
Only support windows-x64, linux-x64 and osx-arm64 now.  
~~Only support FAME scheme now~~
# Support Schemes
## CP-ABE
### BDABE CP-ABE

Georg Bramm, Mark Gall, Julian Schütte , "Blockchain based Distributed Attribute-based Encryption". In Proceedings of the 15th International Joint Conference on e-Business and Telecommunications (ICETE 2018) - Volume 2: SECRYPT, pages 99-110. Available from https://doi.org/10.5220/0006852602650276

### AC17 CP-ABE

Shashank Agrawal, Melissa Chase, "FAME: Fast Attribute-based Message Encryption", (Section 3). In Proceedings of the 2017 ACM SIGSAC Conference on Computer and Communications Security 2017. Available from https://eprint.iacr.org/2017/807.pdf

### AW11 CP-ABE

Lewko, Allison, and Brent Waters, "Decentralizing Attribute-Based Encryption.", (Appendix D). In Eurocrypt 2011. Available from http://eprint.iacr.org/2010/351.pdf

### BSW CP-ABE

John Bethencourt, Amit Sahai, Brent Waters, "Ciphertext-Policy Attribute-Based Encryption" In IEEE Symposion on Security and Privacy, 2007. Available from https://doi.org/10.1109/SP.2007.11

### MKE08 CP-ABE

S Müller, S Katzenbeisser, C Eckert , "Distributed Attribute-based Encryption". Published in International Conference on Information Security and Cryptology, Heidelberg, 2008. Available from http://www2.seceng.informatik.tu-darmstadt.de/assets/mueller/icisc08.pdf


# KP-ABE

### AC17 KP-ABE

Shashank Agrawal, Melissa Chase, "FAME: Fast Attribute-based Message Encryption". In Proceedings of the 2017 ACM SIGSAC Conference on Computer and Communications Security 2017. Available from https://eprint.iacr.org/2017/807.pdf

### LSW KP-ABE

Allison Lewko, Amit Sahai and Brent Waters, "Revocation Systems with Very Small Private Keys". In IEEE Symposium on Security and Privacy, 2010. SP'10. Available from http://eprint.iacr.org/2008/309.pdf

### YCT14 KP-ABE

Xuanxia Yao, Zhi Chen, Ye Tian, "A lightweight attribute-based encryption scheme for the Internet of things". In Future Generation Computer Systems. Available from http://www.sciencedirect.com/science/article/pii/S0167739X14002039

## Getting started
Rabe.Core can be installed from [nuget.org](https://www.nuget.org/packages/Rabe.Core/).
```
dotnet add package Rabe.Core
```
## Getting help
To learn more about Rabe.Core, check out the [Test Example](https://github.com/Aya0wind/Rabe.Core/tree/main/Test)


## Build From source
1. clone project with native library submodule
   + ```git clone https://github.com/Aya0wind/Rabe.Core.git --recursive```

2. Build rust native library 
   + ```cd Rabe.Core/rust```
   + ```cargo build --release```
3. Copy native runtime library to Rabe.Core(Windows example)
   + ```cp target/release/librabe_core.dll ../Rabe.Core/Rabe/libs/runtimes/win-x64/native/rabe_core.dll```
3. Build project
   + Install [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
   + ```dotnet build -c Release```
