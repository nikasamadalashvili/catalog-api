using AutoMapper;
using Catalog.Domain.Entities;
using Catalog.Domain.Reponses;
using Catalog.Domain.Repositories;
using Catalog.Domain.Requests.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemResponse>> GetItemsAsync()
        {
            var result = await _itemRepository.GetAsync();

            return result.Select(x => _mapper.Map<ItemResponse>(x));
        }

        public async Task<ItemResponse> GetItemAsync(GetItemRequest request)
        {
            if (request?.Id == null) 
                throw new ArgumentNullException();

            var entity = await _itemRepository.GetAsync(request.Id);

            return _mapper.Map<ItemResponse>(entity);
        }

        public async Task<ItemResponse> AddItemAsync(AddItemRequest request)
        {
            var item = _mapper.Map<Item>(request);

            var result = _itemRepository.Add(item);
            await _itemRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<ItemResponse>(result);
        }

        public async Task<ItemResponse> EditItemAsync(EditItemRequest request)
        {
            var existingRecord = await _itemRepository.GetAsync(request.Id);

            if (existingRecord == null)
            {
                throw new ArgumentException($"Entity with {request.Id} is not present");
            }

            var entity = _mapper.Map<Item>(request);
            var result = _itemRepository.Update(entity);

            await _itemRepository.UnitOfWork.SaveChangesAsync();
            return _mapper.Map<ItemResponse>(result);
        }
    }
}
