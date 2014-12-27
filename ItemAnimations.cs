using System;

public class ItemAnimations
{
	public readonly static string[] MELEE_ANIMATIONS;

	public readonly static string[] ITEM_ANIMATIONS;

	public readonly static string[] GUN_ANIMATIONS;

	public readonly static string[] HOLD_ANIMATIONS;

	public readonly static string[] OPTIC_ANIMATIONS;

	public readonly static string[] REFILLABLE_ANIMATIONS;

	public readonly static string[] CHART_ANIMATIONS;

	static ItemAnimations()
	{
		ItemAnimations.MELEE_ANIMATIONS = new string[] { "equip", "swingWeak", "swingStrong" };
		ItemAnimations.ITEM_ANIMATIONS = new string[] { "equip", "use" };
		ItemAnimations.GUN_ANIMATIONS = new string[] { "equip", "shootHip", "startAim", "shootAim", "stopAim", "reload", "cock", "startSprint", "stopSprint", "startAttach", "stopAttach" };
		ItemAnimations.HOLD_ANIMATIONS = new string[] { "equip", "start", "stop" };
		ItemAnimations.OPTIC_ANIMATIONS = new string[] { "equip", "startLook", "stopLook" };
		ItemAnimations.REFILLABLE_ANIMATIONS = new string[] { "equip", "use", "drink" };
		ItemAnimations.CHART_ANIMATIONS = new string[] { "equip" };
	}

	public ItemAnimations()
	{
	}

	public static string[] getAnimations(int id)
	{
		int type = ItemType.getType(id);
		switch (type)
		{
			case 21:
			{
				return ItemAnimations.HOLD_ANIMATIONS;
			}
			case 24:
			{
				return ItemAnimations.OPTIC_ANIMATIONS;
			}
			case 26:
			{
				return ItemAnimations.REFILLABLE_ANIMATIONS;
			}
			case 28:
			{
				return ItemAnimations.CHART_ANIMATIONS;
			}
			default:
			{
				if (type == 7)
				{
					break;
				}
				else
				{
					if (type == 8)
					{
						return ItemAnimations.MELEE_ANIMATIONS;
					}
					return ItemAnimations.ITEM_ANIMATIONS;
				}
			}
		}
		return ItemAnimations.GUN_ANIMATIONS;
	}

	public static int getSource(int id)
	{
		int num = id;
		switch (num)
		{
			case 14002:
			{
				return 14001;
			}
			case 14003:
			{
				return 14001;
			}
			case 14004:
			{
				return 14001;
			}
			case 14005:
			{
				return 14001;
			}
			case 14006:
			{
				return 14001;
			}
			case 14007:
			{
				return 14001;
			}
			case 14008:
			{
				return 14001;
			}
			case 14009:
			{
				return 14001;
			}
			case 14010:
			{
				return 14001;
			}
			case 14011:
			{
				return 14001;
			}
			case 14012:
			{
				return 14001;
			}
			case 14013:
			{
				return 14001;
			}
			case 14014:
			{
				return 14000;
			}
			case 14015:
			{
				return 14000;
			}
			case 14016:
			{
				return 14000;
			}
			case 14017:
			{
				return 14000;
			}
			case 14018:
			{
				return 14000;
			}
			case 14019:
			{
				return 14000;
			}
			case 14020:
			{
				return 14001;
			}
			case 14021:
			{
				return 14001;
			}
			case 14022:
			{
				return 14001;
			}
			case 14023:
			{
				return 14001;
			}
			case 14024:
			{
				return 14001;
			}
			case 14025:
			{
				return 14001;
			}
			case 14026:
			{
				return 14001;
			}
			case 14027:
			{
				return 14001;
			}
			case 14028:
			{
				return 14001;
			}
			case 14029:
			{
				return 14001;
			}
			case 14030:
			{
				return 14001;
			}
			case 14031:
			{
				return 14001;
			}
			case 14032:
			{
				return 14001;
			}
			case 14033:
			{
				return 14001;
			}
			default:
			{
				switch (num)
				{
					case 16001:
					{
						return 16000;
					}
					case 16002:
					{
						return 17003;
					}
					case 16005:
					{
						return 16000;
					}
					case 16006:
					{
						return 16003;
					}
					case 16007:
					{
						return 16000;
					}
					case 16008:
					{
						return 16003;
					}
					case 16009:
					{
						return 16000;
					}
					case 16010:
					{
						return 16000;
					}
					case 16011:
					{
						return 16000;
					}
					case 16012:
					{
						return 16000;
					}
					case 16013:
					{
						return 16000;
					}
					case 16014:
					{
						return 16000;
					}
					case 16016:
					{
						return 16015;
					}
					case 16017:
					{
						return 16015;
					}
					case 16018:
					{
						return 16000;
					}
					case 16019:
					{
						return 16000;
					}
					case 16020:
					{
						return 16004;
					}
					case 16021:
					{
						return 16004;
					}
					case 16022:
					{
						return 16000;
					}
					case 16023:
					{
						return 16000;
					}
					case 16024:
					{
						return 16000;
					}
					case 16025:
					{
						return 16000;
					}
					case 16026:
					{
						return 17003;
					}
					case 16027:
					{
						return 16000;
					}
					case 16028:
					{
						return 16000;
					}
					default:
					{
						switch (num)
						{
							case 17000:
							{
								return 16000;
							}
							case 17001:
							{
								return 16000;
							}
							case 17002:
							{
								return 16000;
							}
							case 17004:
							{
								return 16000;
							}
							case 17005:
							{
								return 16000;
							}
							case 17006:
							{
								return 16000;
							}
							case 17007:
							{
								return 16000;
							}
							case 17008:
							{
								return 16000;
							}
							case 17009:
							{
								return 16000;
							}
							case 17010:
							{
								return 16000;
							}
							case 17011:
							{
								return 17003;
							}
							case 17012:
							{
								return 16000;
							}
							case 17013:
							{
								return 16000;
							}
							case 17014:
							{
								return 17003;
							}
							case 17015:
							{
								return 16000;
							}
							case 17016:
							{
								return 16000;
							}
							case 17017:
							{
								return 16000;
							}
							case 17018:
							{
								return 17003;
							}
							case 17019:
							{
								return 16000;
							}
							case 17020:
							{
								return 16000;
							}
							case 17021:
							{
								return 16000;
							}
							default:
							{
								switch (num)
								{
									case 4001:
									{
										return 4000;
									}
									case 4002:
									{
										return 4000;
									}
									case 4003:
									{
										return 4000;
									}
									case 4004:
									{
										return 4000;
									}
									case 4005:
									{
										return 4000;
									}
									case 4006:
									{
										return 4000;
									}
									case 4007:
									{
										return 4000;
									}
									case 4008:
									{
										return 4000;
									}
									case 4009:
									{
										return 4000;
									}
									case 4010:
									{
										return 4000;
									}
									case 4011:
									{
										return 4000;
									}
									case 4012:
									{
										return 4000;
									}
									case 4013:
									{
										return 4000;
									}
									case 4014:
									{
										return 4000;
									}
									case 4015:
									{
										return 4000;
									}
									case 4016:
									{
										return 4000;
									}
									case 4017:
									{
										return 4000;
									}
									case 4018:
									{
										return 4000;
									}
									case 4019:
									{
										return 4000;
									}
									case 4020:
									{
										return 4000;
									}
									default:
									{
										switch (num)
										{
											case 5001:
											{
												return 5000;
											}
											case 5002:
											{
												return 5000;
											}
											case 5003:
											{
												return 5000;
											}
											case 5004:
											{
												return 5000;
											}
											case 5005:
											{
												return 5000;
											}
											case 5006:
											{
												return 5000;
											}
											case 5007:
											{
												return 5000;
											}
											case 5008:
											{
												return 5000;
											}
											case 5009:
											{
												return 5000;
											}
											case 5010:
											{
												return 5000;
											}
											case 5011:
											{
												return 5000;
											}
											case 5012:
											{
												return 5000;
											}
											case 5013:
											{
												return 5000;
											}
											case 5014:
											{
												return 5000;
											}
											case 5015:
											{
												return 5000;
											}
											case 5016:
											{
												return 5000;
											}
											case 5017:
											{
												return 5000;
											}
											case 5018:
											{
												return 5000;
											}
											case 5019:
											{
												return 5000;
											}
											case 5020:
											{
												return 5000;
											}
											default:
											{
												switch (num)
												{
													case 2:
													{
														return 1;
													}
													case 3:
													{
														return 1;
													}
													case 4:
													{
														return 1;
													}
													case 5:
													{
														return 1;
													}
													case 6:
													{
														return 1;
													}
													case 7:
													{
														return 1;
													}
													case 8:
													{
														return 1;
													}
													case 9:
													{
														return 1;
													}
													case 10:
													{
														return 0;
													}
													case 11:
													{
														return 1;
													}
													case 12:
													{
														return 1;
													}
													case 13:
													{
														return 1;
													}
													case 14:
													{
														return 1;
													}
													case 15:
													{
														return 1;
													}
													case 16:
													{
														return 1;
													}
													case 17:
													{
														return 1;
													}
													case 18:
													{
														return 1;
													}
													case 19:
													{
														return 1;
													}
													default:
													{
														switch (num)
														{
															case 8002:
															{
																return 8000;
															}
															case 8006:
															{
																return 8005;
															}
															case 8008:
															{
																return 8001;
															}
															case 8009:
															{
																return 8000;
															}
															case 8011:
															{
																return 8003;
															}
															case 8012:
															{
																return 8004;
															}
															case 8013:
															{
																return 8000;
															}
															case 8014:
															{
																return 8000;
															}
															case 8017:
															{
																return 8003;
															}
															case 8018:
															{
																return 8005;
															}
															case 8019:
															{
																return 8004;
															}
															default:
															{
																switch (num)
																{
																	case 13004:
																	{
																		return 13003;
																	}
																	case 13005:
																	{
																		return 13003;
																	}
																	case 13006:
																	{
																		return 13001;
																	}
																	case 13008:
																	{
																		return 13001;
																	}
																	case 13009:
																	{
																		return 13007;
																	}
																	case 13010:
																	{
																		return 13007;
																	}
																	case 13011:
																	{
																		return 14001;
																	}
																	case 13012:
																	{
																		return 14001;
																	}
																	case 13013:
																	{
																		return 14001;
																	}
																	case 13014:
																	{
																		return 14001;
																	}
																	case 13015:
																	{
																		return 14001;
																	}
																	case 13016:
																	{
																		return 14001;
																	}
																	case 13017:
																	{
																		return 13003;
																	}
																	case 13018:
																	{
																		return 13007;
																	}
																	default:
																	{
																		switch (num)
																		{
																			case 15001:
																			{
																				return 14000;
																			}
																			case 15002:
																			{
																				return 14001;
																			}
																			case 15003:
																			{
																				return 15000;
																			}
																			case 15004:
																			{
																				return 15000;
																			}
																			case 15005:
																			{
																				return 14001;
																			}
																			case 15006:
																			{
																				return 15000;
																			}
																			case 15007:
																			{
																				return 15000;
																			}
																			case 15008:
																			{
																				return 14001;
																			}
																			case 15009:
																			{
																				return 15000;
																			}
																			default:
																			{
																				switch (num)
																				{
																					case 23001:
																					{
																						return 23000;
																					}
																					case 23002:
																					{
																						return 23000;
																					}
																					case 23003:
																					{
																						return 23000;
																					}
																					case 23004:
																					{
																						return 23000;
																					}
																					case 23005:
																					{
																						return 23000;
																					}
																					case 23006:
																					{
																						return 23000;
																					}
																					case 23008:
																					{
																						return 23007;
																					}
																					default:
																					{
																						switch (num)
																						{
																							case 2001:
																							{
																								return 2000;
																							}
																							case 2002:
																							{
																								return 2000;
																							}
																							case 2003:
																							{
																								return 2000;
																							}
																							case 2004:
																							{
																								return 2000;
																							}
																							case 2005:
																							{
																								return 2000;
																							}
																							case 2006:
																							{
																								return 2000;
																							}
																							default:
																							{
																								switch (num)
																								{
																									case 3000:
																									{
																										return 2000;
																									}
																									case 3001:
																									{
																										return 2000;
																									}
																									case 3002:
																									{
																										return 2000;
																									}
																									case 3003:
																									{
																										return 2000;
																									}
																									case 3004:
																									{
																										return 2000;
																									}
																									case 3005:
																									{
																										return 2000;
																									}
																									default:
																									{
																										switch (num)
																										{
																											case 22000:
																											{
																												return 16003;
																											}
																											case 22001:
																											{
																												return 16003;
																											}
																											case 22002:
																											{
																												return 16003;
																											}
																											case 22003:
																											{
																												return 16003;
																											}
																											case 22004:
																											{
																												return 16003;
																											}
																										}
																										break;
																									}
																								}
																								break;
																							}
																						}
																						break;
																					}
																				}
																				break;
																			}
																		}
																		break;
																	}
																}
																break;
															}
														}
														break;
													}
												}
												break;
											}
										}
										break;
									}
								}
								break;
							}
						}
						break;
					}
				}
				break;
			}
		}
		return id;
	}
}