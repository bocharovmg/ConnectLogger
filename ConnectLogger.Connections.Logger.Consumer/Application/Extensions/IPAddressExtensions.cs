using System.Net;
using System.Text;


namespace ConnectLogger.Connections.Logger.Consumer.Application.Extensions;

public static class IPAddressExtensions
{
    public static string Get128BitString(this IPAddress ipAddress)
    {
        var bytes = ipAddress.MapToIPv6().GetAddressBytes();
        StringBuilder ip6Address = new StringBuilder();
        for (int i = 0; i < bytes.Length; i += 2)
        {
            // Преобразуем два байта в 4 шестнадцатеричных символа
            ushort segment = (ushort)((bytes[i] << 8) | bytes[i + 1]);
            ip6Address.AppendFormat("{0:x}", segment); // Добавляем 4 символа

            // Добавляем разделитель ":", кроме последнего сегмента
            if (i < bytes.Length - 2)
            {
                ip6Address.Append(":");
            }
        }

        return ip6Address.ToString();
    }
}
