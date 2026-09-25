# DRPT

DR Planning Tool (DRPT) 2.x: a C# WinForms app that maps business applications, services and servers into disaster recovery plans. It pulls the application and server inventory from an Orbus iServer repository, works out recovery order with a dependency (topological) sort, and produces runbooks, SRM runbooks, dependency maps and recovery-time estimates. It can also query System Center Operations Manager for server health and generate SCOM / SCSM distributed-application management packs for each business application. It was built for infrastructure architects planning DR for an enterprise Windows / VMware estate.

**Source last updated:** 2016-02-03 · **Language:** C# · **Target framework:** .NET Framework 4.6.1 (app), .NET Framework 4.0 (libraries) · **Output:** WinForms exe (`DR Planning Tool.exe`, version 2.16.1.80) plus class libraries and an InstallShield Express setup project

## Solution structure

| Project | Language | Type | Purpose |
|---|---|---|---|
| `DR Tool` (DR Planning Tool) | C# | WinForms exe (.NET 4.6.1) | Main UI: MDI or single-window shell, business application / server / list / drawing forms, dependency map, runbook export, simulation, recovery estimates, options |
| `ISA.Dependency` | C# | Class library (.NET 4.0) | Domain model: business applications, services, servers, disks, HA groups, physical sites, relationships, recovery tasks, runbooks, topological sort |
| `ISA.Orbus` | C# | Class library (.NET 4.0) | Data layer for the Orbus iServer repository database (loads applications, servers, sites and attributes) |
| `ISA.SystemCenter` | C# | Class library (.NET 4.0) | SCOM / SCSM integration via the System Center 2012 R2 SDK: server health, component lookup, management pack creation and sealing |
| `Setup` | InstallShield | InstallShield 2015 Limited Edition (Express) project | Builds the DRPT installer (`Setup.isl`) |
| `Management Packs` | XML | Solution folder | Core customisations management pack and public signing key token used when sealing generated packs |

`DR Tool/Templates` holds the management pack XML templates, and `DR Tool/Database` has the scripts that create and seed the LocalDB config database (`Create Config Tables.sql`, `Populate Default Config data.sql`).

## How to open

Open `DR Planning Tool.sln` at the repository root and set **DR Planning Tool** as the startup project.

The solution also references projects and binaries from sibling folders that are **not in this repository**. In the original layout they sat next to `Solution`:

- `..\Libraries\` - `ISA.Helper`, `ISA.Database`, `ISA.DataLayer`, `ISA.CommandLine`, `HtmlRichTextBox`, `Microsoft.AGL` (MSAGL: `Microsoft.AGL`, `Microsoft.AGL.Drawing`, `Microsoft.AGL.GraphViewerGdi`) and `ReadOnlyPropertyGrid\Rajeev.Windows.Forms`
- `..\2012 R2 SDK Binaries\` - `Microsoft.EnterpriseManagement.Core.dll` and `Microsoft.EnterpriseManagement.OperationsManager.dll`

To build it, put those folders back as siblings of the repository folder, or repoint the references. You can unload the `Setup` project if you don't have InstallShield.

## Requirements

- Visual Studio 2015 (the solution was saved by VS 2015, and the projects use MSBuild ToolsVersion 12.0). It also opens in Visual Studio 2013 to 2019. In Visual Studio 2022 or 2026, retarget the .NET Framework 4.0 libraries to 4.6.1 or later, because the 4.0 targeting pack is no longer installed there.
- .NET Framework 4.6.1 Developer Pack (targeting pack) for the app, and .NET Framework 4.0 for the libraries
- NuGet package restore: `AsyncBridge` 0.1.1
- System Center 2012 R2 Operations Manager SDK assemblies (`Microsoft.EnterpriseManagement.Core` / `.OperationsManager`, found with the SCOM 2012 R2 console) and Windows PowerShell (`System.Management.Automation`)
- SQL Server Express LocalDB (`(localdb)\v11.0`, SQL Server 2012 or later) for the config database. Create it from the `.sql` scripts because the `.mdf` files are not included.
- Optional: InstallShield 2015 Limited Edition for Visual Studio, to build the `Setup` project
- At run time: access to an Orbus iServer database and, if you want the System Center features, SCOM / SCSM 2012 R2 management servers

## Attribution and provenance

- Working copy from my Development folder `DRPT/DRPT (v2)/Solution`.
- Written by me (Dave Robinson) and originally released under the ISA Technologies name. The assemblies carry `AssemblyCompany("Dave Robinson")` and `Copyright © Dave Robinson 2015–2016`, and the product name is "DR Planning Tool" (assembly version 2.16.1.80). Source control bindings point to a Team Foundation Server.
- Other DRPT copies exist in the same OneDrive folder and were intentionally **not** included: `DRPT (v1)`, `DRPT feb 2016`, the `DRPT 2.x (latest)` folder (it only holds the files the InstallShield installer deploys, not source), plus a `Backup` folder. This repository is the 2.x-era source from `DRPT (v2)`.
- Third-party components are listed in [THIRD_PARTY_NOTICES.md](THIRD_PARTY_NOTICES.md).

### Left out of this import

- Build output and tooling folders: `bin`, `obj`, `.vs`, `packages`, `.nugetaudit`, the InstallShield build output under `Setup/Setup`, the `.suo` / `.csproj.user` files, and the TFS `.vspscc` / `.vssscc` binding files, `UpgradeLog.htm`, the `Setup.isl.774` backup and a zero-byte Paint.NET temp file
- SQL Server database files (`DRPTConfig*.mdf` / `.ldf`). Rebuild them from `DR Tool/Database/*.sql`.
- The private strong-name key `Management Packs/isa.snk`. Only the public key `isapublic.snk` and its token are kept. Use your own key pair to seal management packs.
- The compiled, sealed pack `ISA.DistributedApplications.Core.Customisations.mp`, plus two client-specific management pack exports (`FIN.DistributedApplications.Core.Customisations.xml` and an SCSM `ServiceManager.LinkingFramework.Configuration.xml` export)
- Client server names, the internal DNS suffix, a UNC settings share and the TFS server URL were replaced with placeholders (`CORPSQLPRD001`, `CORPSCOMPRD001`, `.CORP.EXAMPLE.ORG`, `\\YourFileServer\YourShare`, `tfs.example.org`), and the default company name was changed to `Your Organisation`. No passwords or credentials were present: connection strings use Windows authentication (`Trusted_Connection`).

## License

MIT - see [LICENSE](LICENSE). Copyright (c) 2026 VaderConsulting. Third-party components keep their own licenses (see [THIRD_PARTY_NOTICES.md](THIRD_PARTY_NOTICES.md)).
