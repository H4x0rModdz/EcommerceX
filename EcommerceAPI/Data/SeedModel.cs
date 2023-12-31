﻿namespace EcommerceAPI.Data;

public class SeedModel
{
    public List<UserSeedData> Users { get; set; }
    public List<ProductSeedData> Products { get; set; }
    public List<TransactionSeedData> Transactions { get; set; }
}