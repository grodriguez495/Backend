using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Users.Commands;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Users.Commands.UpdateUsers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,UserDto>
{
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IMapper _mapper;
    public UpdateUserCommandHandler(IUserCommandRepository userCommandRepository,IUserQueryRepository userQueryRepository, IMapper mapper)
    {
        _userCommandRepository = userCommandRepository ?? throw new ArgumentNullException(nameof(userCommandRepository));
        _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var currentUser = await _userQueryRepository.FindAsync(request.UserId,false, cancellationToken);
            currentUser = UpdateRecords(currentUser,request);
            await _userCommandRepository.EditAsync(currentUser, cancellationToken: cancellationToken);
            var user = await _userQueryRepository.FindAsync(request.UserId, false, cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new UserDto();
        }
    }

    private User UpdateRecords(User currentUser, UpdateUserCommand request)
    {
        currentUser.Name = request.Name;
        currentUser.Password = UpdateMD5(request.Password);
        currentUser.RoleId = request.RoleId;
        currentUser.Email = request.Email;
        currentUser.Phone = request.Phone;
        currentUser.IsActive = request.IsActive;
        return currentUser;
    }

    private string UpdateMD5(string requestPassword)
    {
        string hash = String.Empty;
        byte[] data = Encoding.UTF8.GetBytes(requestPassword);
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        TripleDESCryptoServiceProvider tripDES = new TripleDESCryptoServiceProvider();
        tripDES.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
        tripDES.Mode = CipherMode.ECB;
        ICryptoTransform transform = tripDES.CreateEncryptor();
        byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
        hash = Convert.ToBase64String(result);
        return hash;
    }
}