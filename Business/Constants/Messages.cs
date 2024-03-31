using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün başarıyla eklendi";
        public static string ProductDeleted = "Ürün başarıyla silindi";
        public static string ProductUpdated = "Ürün başarıyla güncellendi";

        public static string CategoryAdded = "Kategori başarıyla eklendi";
        public static string CategoryDeleted = "Kategori başarıyla silindi";
        public static string CategoryUpdated = "Kategori başarıyla güncellendi";

        public static string NoProduct = "Böyle bir ürün bulunmamaktadır";
        public static string NoCategory = "Böyle bir kategori bulunmamaktadır";
        public static string ProductAddedAndLive = "Ürün başarıyla eklendi ve yayına alındı";
        public static string ProductNotReleased = "Ürün başarıyla eklendi fakat stok miktarınız kategori limitinin altında olduğundan ürününüz yayına alınamadı";

    }
}
