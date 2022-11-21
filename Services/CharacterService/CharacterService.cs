using AutoMapper;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>(){
            new Character(),
            new Character{Name = "Sam", Id = 1}
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>>
            {
                Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            


            try
            {
                Character character = characters.FirstOrDefault(c => c.Id == id) ?? new Character();
            

                if (character != null)
                {

                    response.Data = _mapper.Map<GetCharacterDto>(character);
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
            try
            {
                Character? character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

                if (character != null)
                {

                    _mapper.Map(updatedCharacter, character);
                    //character.Name = updatedCharacter.Name;
                    //character.Hitpoints = updatedCharacter.Hitpoints;
                    //character.Strength = updatedCharacter.Strength;
                    //character.Defense = updatedCharacter.Defense;
                    //character.Intelligence = updatedCharacter.Intelligence;
                    //character.Class = updatedCharacter.Class;

                    response.Data = _mapper.Map<GetCharacterDto>(character);
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
                Character? character = characters.FirstOrDefault(c => c.Id == id);

                if (character != null)
                {

                    characters.Remove(character);

                    response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
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