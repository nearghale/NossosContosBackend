using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Entities
{
    public class Media : MongoDB.Base
    {
		public Media(Types type, string extension, string baseUrl)
		{
			this.Key = Guid.NewGuid();
			this.Type = type;
			this.Extension = extension.Replace(".", "");
			this.Url = $"{baseUrl}/{this.Key.ToString()}.{this.Extension}";
		}

		public enum Types
		{
			Unknown = 0,
			Image = 1,
			Video = 2,
			File = 3,
			Audio = 4
		}

		[JsonProperty("user_id")]
		public Guid UserId { get; set; }

		[JsonProperty("type_id")]
		public int TypeId { get; set; }

		[JsonProperty("key")]
		public Guid Key { get; set; }

		[JsonProperty("extension")]
		public string Extension { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonIgnore]
		[BsonIgnore]
		public string Name
		{
			get { return $"{Key.ToString()}.{Extension}"; }
		}

		[JsonIgnore]
		[BsonIgnore]
		public Types Type
		{
			get { return (Types)this.TypeId; }
			set { this.TypeId = (int)value; }
		}

	}
}
