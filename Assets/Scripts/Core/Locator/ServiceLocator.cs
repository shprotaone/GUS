using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GUS.Core.Locator
{
    public class ServiceLocator : IServiceLocator
    {
        public static ServiceLocator ServiceLocatorInstance { get; private set; }
        private readonly Dictionary<Type,List<object>> services = new Dictionary<Type, List<object>>();

        public void Register<TService>(TService service)
        {
            Type key = typeof(TService);
            if (!services.ContainsKey(key))             //�������� �� ������� �������
                services.Add(key, new List<object>());  //���������� �������, ���� ��� ���
            services[key].Add(service);                 //���������� ������� ����������� ���� � �����
        }

        public TService Get<TService>()
        {
            Console.WriteLine(typeof(TService).Name);
            if (services.ContainsKey(typeof(TService)))
                return (TService)services[typeof(TService)].Single();
            else
                throw new InvalidOperationException("Can't resolve service " + typeof(TService));
        }

        public IEnumerable<TService> GetAll<TService>()
        {
            return services[typeof(TService)].Cast<TService>();
        }
    }
}

