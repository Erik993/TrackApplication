Comments:
1) Intrface - to define what operations exists,  to allow DI
2) Repository that implements interface - Database logic. Scoped lifetime
3) States - Holds data that multiple ViewModels shuld have access to. Singleton.
4) ViewModel - handle UI, repare data for UI, loads data into state. Transcient lifetime

test project has Microsoft.EntityFrameworkCore.InMemory installed