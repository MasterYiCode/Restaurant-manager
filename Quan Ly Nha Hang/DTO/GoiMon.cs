using System;
using System.Collections.Generic;


namespace Quan_Ly_Nha_Hang.DTO
{
    public class GoiMon
    {
       
        public string Maphucvu { get; set; }
        public List<MonAn> Order { get; set; }
        public double Tongthanhtoanhientai { get; set; }

        public GoiMon(string maphucvu, List<MonAn> dsmaOrder, double tongthanhtoanhientai)
        {
            Maphucvu = maphucvu;
            Order = dsmaOrder;
            Tongthanhtoanhientai = tongthanhtoanhientai;
        }

        //Món ăn:  mã món, tên món, đơn giá, số lượng, loại món
        // Phục vụ: mã phục vụ, mã bàn, mã nhân viên, tên nhân viên, mã khách hàng, thời gian vào
        // Gọi món: thì cần có : mã phục vụ,(mã món, số lượng, giá món)( .... ) (.....), tổng thanh toán hiện tại
        // Sử dụng: mã phục vụ, Dictonary(key, MonAn), thanh toán hiện tại
        // Xử lý: mã phục vụ phải chưa tồn tại hoặc tồn tại rồi.
        // Dictornary(key, MonAn); int key sẽ là số lượng món ăn gọi. Mỗi lần gọi sẽ tính tổng thanh toán hiện tại. 

    }
}
