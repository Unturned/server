using System;
using System.Reflection;
using UnityEngine;

namespace CommandHandler {
	public class BetterNetworkUser {
		/// <summary>
		/// Returns the player name
		/// </summary>
		public string name {
			get {
				return realUser.name;
			}
			
			set {
				realUser.name = value;
			}
		}

		/// <summary>
		/// Returns the player nickname. This is usually empty unless the player picked one
		/// </summary>
		public string nickname {
			get {
				return realUser.nickname;
			}
			set {
				realUser.nickname = value;
			}
		}

		/// <summary>
		/// Returns the player's group
		/// </summary>
		public string friend {
			get { return realUser.friend; }
			set { realUser.friend = value; }
		}

		/// <summary>
		/// Returns the player's Steam64ID
		/// </summary>
		public string steamid {
			get { return realUser.id; }
			set { realUser.id = value; }
		}

		/// <summary>
		/// Returns the player's status
		/// </summary>
		public int status {
			get { return realUser.status; }
			set { realUser.status = value; }
		}

		/// <summary>
		/// Returns the player's reputation. Between -100 and 100
		/// </summary>
		public int reputation {
			get { return realUser.reputation; }
			set { realUser.reputation = value; }
		}

		/// <summary>
		/// Returns the player's  NetworkPlayer 
		/// </summary>
		public NetworkPlayer networkPlayer {
			get { return realUser.player; }
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
			get { return realUser.model; }
		}

		/// <summary>
		/// Returns the time at which time the player spawned
		/// </summary>
		public float spawned {
			get { return realUser.spawned; }
		}

		/// <summary>
		/// Returns wether the player is muted or not
		/// </summary>
		public bool muted {
			get { return realUser.muted; }
			set { realUser.muted = value; }
		}

		/// <summary>
		/// Returns the actual NetworkUser which was used for this object
		/// </summary>
		public NetworkUser realUser { get; private set; }

		public BetterNetworkUser (NetworkUser user) {
			realUser = user;
		}

		public String toString() {
			return name;
		}
	}
}
