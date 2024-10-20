﻿namespace Inventory_Management_System.VerticalSlice.Features.Products.UpdateProduct
{
    public class UpdateProductEndPointRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int LowStockThreshold { get; set; }
    }
}
