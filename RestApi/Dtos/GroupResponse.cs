namespace RestApi.Dtos {
    public class GroupResponse {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<UserResponse> Users { get; set; }
        public DateTime CreationDate { get; set; }  
    }
}