using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            response.Data = await _context.Characters
                .Select(c => _mapper.Map<GetCharacterDto>(c))
                .ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();

            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

            try
            {

                if (dbCharacter != null)
                {

                    response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Character with Id {id} not found.";
                }


            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
            try
            {

                if (dbCharacter != null)
                {

                    _mapper.Map(updatedCharacter, dbCharacter);
                    //character.Name = updatedCharacter.Name;
                    //character.Hitpoints = updatedCharacter.Hitpoints;
                    //character.Strength = updatedCharacter.Strength;
                    //character.Defense = updatedCharacter.Defense;
                    //character.Intelligence = updatedCharacter.Intelligence;
                    //character.Class = updatedCharacter.Class;
                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Character with Id {updatedCharacter.Id} not found.";
                }


            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }


            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var dBCharacter = await _context.Characters.FirstAsync(c => c.Id == id);

                if (dBCharacter != null)
                {

                    _context.Characters.Remove(dBCharacter);
                    await _context.SaveChangesAsync();

                    response.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Character with Id {id} not found.";
                }


            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }


            return response;
        }
    }
}