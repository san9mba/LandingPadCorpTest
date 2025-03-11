using System;

namespace BusinessEntities.Sales
{
    public class Product : IdNameObject
    {
        private DateTime? _deletedDate = null;
        private int _stock;

        public string Description { get; set; }
        public Money Price { get; set; }
        public int Stock
        {
            get => _stock;
            set => _stock = value < 0 ? 0 : value;
        }

        public DateTime? DeletedDate
        {
            get => _deletedDate;
            set
            {
                // can't set new date. only reset to null or set rather then null
                if (_deletedDate.HasValue && value.HasValue)
                    throw new ArgumentException($"{nameof(DeletedDate)} already has a value.");
                _deletedDate = value;
            }
        }
    }
}
