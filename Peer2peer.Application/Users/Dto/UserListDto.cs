using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Peer2peer.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserListDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }


        public string ProfileImage { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string Score { get; set; }
        public string Status { get; set; }
        public int Circel { get; set; }
    }
}