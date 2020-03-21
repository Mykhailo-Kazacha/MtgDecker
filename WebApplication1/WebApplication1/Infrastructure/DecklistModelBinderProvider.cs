using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Infrastructure
{
    public class DecklistModelBinderProvider : IModelBinderProvider
    {
        private MtgDeckerContext db;

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(Decklist))
            {
                // Для объекта SimpleTypeModelBinder необходим сервис ILoggerFactory
                // Получаем его из сервисов          
                IModelBinder binder = new DecklistModelBinder(db);
                return binder;
            }

            return null;

        }
    }
}
