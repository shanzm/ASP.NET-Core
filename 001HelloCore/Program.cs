using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/*
 �о�.net Core ����Ҫ�о����������̣�����������ע�룬�����������ܵ����м����Ӧ�����ã��໷������־��·�ɣ���̬�ļ�

.net core 3.1 �ǿ��Կ���winform ��webForm�ģ�����Ŀǰ�����ܿ�ƽ̨��

����Ҫ���ף�.net core��.net�ĺ���API��
.net extensions��.net core ����չ�������� ����.net�Ĺ����������������־������ע�롢Ӧ�ó������õ�


.net core extensions��Դ�룺https://github.com/dotnet/extensions
��¡������ô���ԣ�

��Ҫֱ�Ӵ򿪽������

�������п�¡�ļ��еģ�restor.cmd-->���У�startvs.cmd-->���У�build.cmd-->�򿪽������

���ǲ�������������Ϊ����Ҫ�������е���Ŀ���Լ��ĵ��Ե��ڴ���ܲ���

���У����Ե����Լ���Ҫ���Ĳ��ִ��룬

�㿪��extension-->src-->ѡ����Ҫ�鿴�Ĳ��֣�����Hosting��ʾ�����ࣩ-->�����Ŀ¼�µ�startvs.cmd

��ʵ�鿴��Ч�������ߵĲ鿴����һģһ����
.net core�������Դ�룺https://source.dot.net/

 */


/*

1. �½���Ŀ������û��Coreģ�壬����������󣬰�װ�µĹ��ߺ�ģ��

2. �½�һ��ASP.NET Core WebӦ�ó���

3. ��һ���½���Ŀ���Ƽ�λ�ã�

4. ��һ��ѡ��.NET Core -->.NET Core 3.1(ע�ⲻ��ʹ��VS2019 v16.4�汾����)
    ѡ�����Ŀģ��

   ע�������� ȡ��ѡ��HTTPS���õĵ�ѡ��

4. ���Ե�ʱ���ڵ��Ե���������ѡ��ǰ��Ŀ���Ƶ�ѡ���ʹ�ÿ���̨����������Ҫ���IIS Express���ԣ�����ʵ���漰�����֣�

    ����ʹ��IIS�������򲻻���ֿ���̨�ĵ�����Ϣ������Ҳ�Ͳ�������Kestrel

 */


namespace _001HelloCore
{

    /// <summary>
    /// ������Program���е�Main()���������ڴ�����������������
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
        //����д������Ĭ�����ɵģ����� Linqд������ʵ�ȼ�������д��

        ///Ĭ����������������Ҫ������������ 
        ///��������Ӧ�õ������������ڵĹ������÷�������������Ĺܵ�
        ///Ĭ��������־��¼��������ϵ��ע�������
        ///������.net core �е�һ���࣬��������Host
        ///������װ��Ӧ����Դ�Ķ���Ӧ���������������еģ������а�����ǰӦ������Ҫ��������Դ

        ///.net core ��������������������(ͨ������ )��web����������ͨ����������չ�����ṩ����web���ܣ�֧��HTTP��


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>//��ǰ׺Ϊ��ASPNETCORE���Ļ�����������WEB�������ã�Ĭ���ǽ�Kestrel����ΪWeb���������������Ĭ�ϵ����ã���Ȼ����Ҳ��������ΪIIS������
                {
                    //�������(�������������������������ã�������չ���ṩ�ķ���)
                    //���������ó�ΪӲ���룬��ʵ�������ö��ǿ����������ļ������õ�
                    webBuilder.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = 1024 * 1024 * 100);//�������������ֵ��100M��Ĭ����28.6M
                    webBuilder.ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));//�����ռǼ�¼����С����

                    //
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:8000");//���ö˿�
                });
        }
    }
}

/*
Kestre��һ����ƽ̨��������ASP.NET Core��Web������
���Լ򵥵Ŀ�����IIS�������书�ܱȽ��٣��ṩ�Ķ���Http����

ע��Kestrel�����Ǽ��ߵģ����ҿ���������Linux��

�����÷��ǣ��������ķ�������������Nginx,IIS,Apache�����ʹ��(�����û����͵�web������ֱ�ӷ��͸����������������ɷ������������ڷ��͸�Kestrel������)


*/
