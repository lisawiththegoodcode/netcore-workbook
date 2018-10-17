using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace TagHelper.Tests.Infrastructure
{
    internal class DefaultMetaDataHelperProvider : IMetadataDetailsProvider
    {
        private string format;

        public DefaultMetaDataHelperProvider(string format)
        {
            this.format = format;
        }
    }
}