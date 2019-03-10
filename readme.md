
# SansAtlas - A replacement framework supporting Controllers and GlassMapper
I was recently tasked with upgrading and modifying a site built with Atlas which was found to be overly restrictive due to it's lack of support for Controllers and a custom Sitecore ORM implementation.

The changes in this version of the framework are:
- Support for the opinionated models from Atlas has been removed in favour of GlassMapper for it's stability and community support.
- Controllers are supported... putting the C back in MVC.

Although it does take some careful work and effort to replace the Sitecore ORM with Glass Mapper compatible models, the implementations are fairly similar so it wasn't too painful or time consuming. Maybe this will be of some help to others in a similar situation.

To upgrade:
- Swap out the assemblies.
- Replace custom ORM models and attributes with GlassMapper models and attributes.
- Replace pipeline processors with GlassMapper pipelines.
- Recompile and test.

# Original Repository

The original repository can be found here: [https://github.com/DeloitteDigitalAPAC/Atlas](https://github.com/DeloitteDigitalAPAC/Atlas)

## LICENSE (BSD-3-Clause)
[View License](LICENSE)
