using System;
using System.Reflection;
using UnityEngine;

namespace CommandHandler {
	public class BetterNetworkUser {
		private static FieldInfo[] fields = typeof(NetworkUser).GetFields();
		
		/// <summary>
		/// Returns the player name
		/// </summary>
		public string name {
			get {
				return (String)fields [0].GetValue(realUser);
			}
			
			set {
				fields [0].SetValue(realUser, value);
			}
		}

		/// <summary>
		/// Returns the player nickname. This is usually empty unless the player picked one
		/// </summary>
		public string nickname {
			get {
				return (String)fields [1].GetValue (realUser);
			}
			set {
				fields[1].SetValue(realUser, value);
			}
		}

		/// <summary>
		/// Returns the player's group
		/// </summary>
		public string friend {
			get { return (String)fields [2].GetValue (realUser); }
			set { fields [2].SetValue (realUser, value); }
		}

		/// <summary>
		/// Returns the player's Steam64ID
		/// </summary>
		public string steamid {
			get { return (String)fields [3].GetValue (realUser); }
			set { fields [3].SetValue (realUser, value); }
		}

		/// <summary>
		/// Returns the player's status
		/// </summary>
		public int status {
			get { return (int)fields [4].GetValue (realUser); }
			set { fields [4].SetValue (realUser, value); }
		}

		/// <summary>
		/// Returns the player's reputation. Between -100 and 100
		/// </summary>
		public int reputation {
			get { return (int)fields [5].GetValue (realUser); }
			set { fields [5].SetValue (realUser, value);      }
		}

		/// <summary>
		/// Returns the player's  NetworkPlayer 
		/// </summary>
		public NetworkPlayer networkPlayer {
			get { return (NetworkPlayer)fields [6].GetValue (realUser); }
		}

		/// <summary>
		/// Gets or sets the player's position
		/// </summary>
		public Vector3 position {
			get {
				return this.player.gameObject.transform.position;
			}
			set {
				this.player.gameObject.transform.position = value;
			}
		}

		/// <summary>
		/// Gets or sets the player's position
		/// </summary>
		public Quaternion rotation {
			get {
				return this.player.gameObject.transform.rotation;
			}
			set {
				this.player.gameObject.transform.rotation = value;
			}
		}

		/// <summary>
		/// Returns the Player object associated with this user
		/// </summary>
		public Player player {
			get {
				Player betterThanNothing = null;
				Player[] players = UnityEngine.Object.FindObjectsOfType (typeof(Player)) as Player[];
				foreach (Player player in players) {
					if (player.name.ToLower ().Equals (this.name.ToLower ())) {
						return player;
					} else if (player.name.ToLower ().Contains (this.name.ToLower ())) {
						betterThanNothing = player;
					}
				}
				return betterThanNothing;
			}
		}

		/// <summary>
		/// Returns the player's gameobject
		/// </summary>
		[Obsolete("NetworkUsers models tends to become null sometimes. Use BetterNetworkUser.player.gameObject instead")]
		public GameObject model {
			get { return (GameObject)fields [7].GetValue (realUser); }
		}

		/// <summary>
		/// Returns the time at which time the player spawned
		/// </summary>
		public float spawned {
			get { return (float)fields [8].GetValue (realUser); }
		}

		/// <summary>
		/// Returns wether the player is muted or not
		/// </summary>
		public bool muted {
			get { return (bool)fields [0].GetValue (realUser); }
			set { fields [0].SetValue (realUser, value); }
		}

		private NetworkUser _realUser;
		/// <summary>
		/// Returns the actual NetworkUser which was used for this object
		/// </summary>
		public NetworkUser realUser {
			get {
				return _realUser;
			}
			private set {
				_realUser = value;
			}
		}

		public BetterNetworkUser (NetworkUser user) {
			realUser = user;
		}

		public String toString() {
			return name;
		}
	}
}
