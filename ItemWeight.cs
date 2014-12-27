using System;
using UnityEngine;

public class ItemWeight
{
	public ItemWeight()
	{
	}

	public static string getText(int amount)
	{
		if (GameSettings.metric)
		{
			return string.Concat((float)Mathf.FloorToInt((float)amount / 10f) / 100f, "kg");
		}
		return string.Concat((float)Mathf.FloorToInt((float)amount / 10f) / 100f * 2.2f, " lb");
	}

	public static int getWeight(int id)
	{
		int num = id;
		switch (num)
		{
			case 14000:
			{
				return 200;
			}
			case 14001:
			{
				return 80;
			}
			case 14002:
			{
				return 50;
			}
			case 14003:
			{
				return 50;
			}
			case 14004:
			{
				return 40;
			}
			case 14005:
			{
				return 40;
			}
			case 14006:
			{
				return 60;
			}
			case 14007:
			{
				return 60;
			}
			case 14008:
			{
				return 40;
			}
			case 14009:
			{
				return 40;
			}
			case 14010:
			{
				return 50;
			}
			case 14011:
			{
				return 50;
			}
			case 14012:
			{
				return 100;
			}
			case 14013:
			{
				return 100;
			}
			case 14014:
			{
				return 200;
			}
			case 14015:
			{
				return 200;
			}
			case 14016:
			{
				return 200;
			}
			case 14017:
			{
				return 200;
			}
			case 14018:
			{
				return 200;
			}
			case 14019:
			{
				return 150;
			}
			case 14020:
			{
				return 150;
			}
			case 14021:
			{
				return 150;
			}
			case 14022:
			{
				return 150;
			}
			case 14023:
			{
				return 100;
			}
			case 14024:
			{
				return 200;
			}
			case 14025:
			{
				return 50;
			}
			case 14026:
			{
				return 50;
			}
			case 14027:
			{
				return 50;
			}
			case 14028:
			{
				return 50;
			}
			case 14029:
			{
				return 50;
			}
			case 14030:
			{
				return 50;
			}
			case 14031:
			{
				return 100;
			}
			case 14032:
			{
				return 100;
			}
			case 14033:
			{
				return 100;
			}
			default:
			{
				switch (num)
				{
					case 16000:
					{
						return 250;
					}
					case 16001:
					{
						return 250;
					}
					case 16002:
					{
						return 400;
					}
					case 16003:
					{
						return 100;
					}
					case 16004:
					{
						return 150;
					}
					case 16005:
					{
						return 200;
					}
					case 16006:
					{
						return 100;
					}
					case 16007:
					{
						return 750;
					}
					case 16008:
					{
						return 50;
					}
					case 16009:
					{
						return 200;
					}
					case 16010:
					{
						return 150;
					}
					case 16011:
					{
						return 200;
					}
					case 16012:
					{
						return 300;
					}
					case 16013:
					{
						return 300;
					}
					case 16014:
					{
						return 200;
					}
					case 16015:
					{
						return 150;
					}
					case 16016:
					{
						return 100;
					}
					case 16017:
					{
						return 125;
					}
					case 16018:
					{
						return 200;
					}
					case 16019:
					{
						return 500;
					}
					case 16020:
					{
						return 500;
					}
					case 16021:
					{
						return 500;
					}
					case 16022:
					{
						return 250;
					}
					case 16023:
					{
						return 300;
					}
					case 16024:
					{
						return 200;
					}
					case 16025:
					{
						return 600;
					}
					case 16026:
					{
						return 150;
					}
					case 16027:
					{
						return 250;
					}
					case 16028:
					{
						return 500;
					}
					default:
					{
						switch (num)
						{
							case 17000:
							{
								return 1000;
							}
							case 17001:
							{
								return 500;
							}
							case 17002:
							{
								return 450;
							}
							case 17003:
							{
								return 250;
							}
							case 17004:
							{
								return 500;
							}
							case 17005:
							{
								return 400;
							}
							case 17006:
							{
								return 800;
							}
							case 17007:
							{
								return 500;
							}
							case 17008:
							{
								return 300;
							}
							case 17009:
							{
								return 150;
							}
							case 17010:
							{
								return 400;
							}
							case 17011:
							{
								return 200;
							}
							case 17012:
							{
								return 250;
							}
							case 17013:
							{
								return 400;
							}
							case 17014:
							{
								return 500;
							}
							case 17015:
							{
								return 600;
							}
							case 17016:
							{
								return 600;
							}
							case 17017:
							{
								return 600;
							}
							case 17018:
							{
								return 500;
							}
							case 17019:
							{
								return 800;
							}
							case 17020:
							{
								return 450;
							}
							case 17021:
							{
								return 600;
							}
							default:
							{
								switch (num)
								{
									case 4000:
									{
										return 200;
									}
									case 4001:
									{
										return 200;
									}
									case 4002:
									{
										return 200;
									}
									case 4003:
									{
										return 200;
									}
									case 4004:
									{
										return 200;
									}
									case 4005:
									{
										return 200;
									}
									case 4006:
									{
										return 200;
									}
									case 4007:
									{
										return 200;
									}
									case 4008:
									{
										return 200;
									}
									case 4009:
									{
										return 200;
									}
									case 4010:
									{
										return 200;
									}
									case 4011:
									{
										return 200;
									}
									case 4012:
									{
										return 200;
									}
									case 4013:
									{
										return 200;
									}
									case 4014:
									{
										return 200;
									}
									case 4015:
									{
										return 200;
									}
									case 4016:
									{
										return 150;
									}
									case 4017:
									{
										return 150;
									}
									case 4018:
									{
										return 150;
									}
									case 4019:
									{
										return 150;
									}
									case 4020:
									{
										return 150;
									}
									default:
									{
										switch (num)
										{
											case 5000:
											{
												return 200;
											}
											case 5001:
											{
												return 200;
											}
											case 5002:
											{
												return 200;
											}
											case 5003:
											{
												return 200;
											}
											case 5004:
											{
												return 200;
											}
											case 5005:
											{
												return 200;
											}
											case 5006:
											{
												return 200;
											}
											case 5007:
											{
												return 200;
											}
											case 5008:
											{
												return 200;
											}
											case 5009:
											{
												return 200;
											}
											case 5010:
											{
												return 200;
											}
											case 5011:
											{
												return 200;
											}
											case 5012:
											{
												return 200;
											}
											case 5013:
											{
												return 200;
											}
											case 5014:
											{
												return 200;
											}
											case 5015:
											{
												return 200;
											}
											case 5016:
											{
												return 150;
											}
											case 5017:
											{
												return 150;
											}
											case 5018:
											{
												return 150;
											}
											case 5019:
											{
												return 150;
											}
											case 5020:
											{
												return 150;
											}
											default:
											{
												switch (num)
												{
													case 18000:
													{
														return 100;
													}
													case 18001:
													{
														return 50;
													}
													case 18002:
													{
														return 50;
													}
													case 18003:
													{
														return 400;
													}
													case 18004:
													{
														return 25;
													}
													case 18005:
													{
														return 100;
													}
													case 18006:
													{
														return 150;
													}
													case 18007:
													{
														return 175;
													}
													case 18008:
													{
														return 250;
													}
													case 18009:
													{
														return 100;
													}
													case 18010:
													{
														return 50;
													}
													case 18011:
													{
														return 75;
													}
													case 18012:
													{
														return 150;
													}
													case 18013:
													{
														return 200;
													}
													case 18014:
													{
														return 250;
													}
													case 18015:
													{
														return 200;
													}
													case 18016:
													{
														return 250;
													}
													case 18017:
													{
														return 100;
													}
													case 18018:
													{
														return 40;
													}
													case 18019:
													{
														return 600;
													}
													case 18020:
													{
														return 250;
													}
													default:
													{
														switch (num)
														{
															case 0:
															{
																return 150;
															}
															case 1:
															{
																return 400;
															}
															case 2:
															{
																return 250;
															}
															case 3:
															{
																return 300;
															}
															case 4:
															{
																return 250;
															}
															case 5:
															{
																return 150;
															}
															case 6:
															{
																return 250;
															}
															case 7:
															{
																return 400;
															}
															case 8:
															{
																return 400;
															}
															case 9:
															{
																return 100;
															}
															case 10:
															{
																return 50;
															}
															case 11:
															{
																return 100;
															}
															case 12:
															{
																return 100;
															}
															case 13:
															{
																return 150;
															}
															case 14:
															{
																return 100;
															}
															case 15:
															{
																return 100;
															}
															case 16:
															{
																return 150;
															}
															case 17:
															{
																return 500;
															}
															case 18:
															{
																return 200;
															}
															case 19:
															{
																return 300;
															}
															default:
															{
																switch (num)
																{
																	case 8000:
																	{
																		return 1000;
																	}
																	case 8001:
																	{
																		return 100;
																	}
																	case 8002:
																	{
																		return 1000;
																	}
																	case 8003:
																	{
																		return 250;
																	}
																	case 8004:
																	{
																		return 300;
																	}
																	case 8005:
																	{
																		return 400;
																	}
																	case 8006:
																	{
																		return 350;
																	}
																	case 8007:
																	{
																		return 200;
																	}
																	case 8008:
																	{
																		return 100;
																	}
																	case 8009:
																	{
																		return 800;
																	}
																	case 8010:
																	{
																		return 500;
																	}
																	case 8011:
																	{
																		return 300;
																	}
																	case 8012:
																	{
																		return 250;
																	}
																	case 8013:
																	{
																		return 350;
																	}
																	case 8014:
																	{
																		return 300;
																	}
																	case 8015:
																	{
																		return 300;
																	}
																	case 8016:
																	{
																		return 250;
																	}
																	case 8017:
																	{
																		return 100;
																	}
																	case 8018:
																	{
																		return 150;
																	}
																	case 8019:
																	{
																		return 200;
																	}
																	default:
																	{
																		switch (num)
																		{
																			case 7000:
																			{
																				return 3000;
																			}
																			case 7001:
																			{
																				return 1000;
																			}
																			case 7002:
																			{
																				return 800;
																			}
																			case 7003:
																			{
																				return 900;
																			}
																			case 7004:
																			{
																				return 500;
																			}
																			case 7005:
																			{
																				return 1200;
																			}
																			case 7006:
																			{
																				return 1000;
																			}
																			case 7007:
																			{
																				return 1500;
																			}
																			case 7008:
																			{
																				return 3500;
																			}
																			case 7009:
																			{
																				return 2500;
																			}
																			case 7010:
																			{
																				return 700;
																			}
																			case 7011:
																			{
																				return 4000;
																			}
																			case 7012:
																			{
																				return 1500;
																			}
																			case 7013:
																			{
																				return 3000;
																			}
																			case 7014:
																			{
																				return 1000;
																			}
																			case 7015:
																			{
																				return 1000;
																			}
																			case 7016:
																			{
																				return 1500;
																			}
																			case 7017:
																			{
																				return 1250;
																			}
																			case 7018:
																			{
																				return 1500;
																			}
																			default:
																			{
																				switch (num)
																				{
																					case 13000:
																					{
																						return 200;
																					}
																					case 13001:
																					{
																						return 150;
																					}
																					case 13002:
																					{
																						return 500;
																					}
																					case 13003:
																					{
																						return 100;
																					}
																					case 13004:
																					{
																						return 80;
																					}
																					case 13005:
																					{
																						return 80;
																					}
																					case 13006:
																					{
																						return 150;
																					}
																					case 13007:
																					{
																						return 250;
																					}
																					case 13008:
																					{
																						return 150;
																					}
																					case 13009:
																					{
																						return 100;
																					}
																					case 13010:
																					{
																						return 100;
																					}
																					case 13011:
																					{
																						return 30;
																					}
																					case 13012:
																					{
																						return 30;
																					}
																					case 13013:
																					{
																						return 30;
																					}
																					case 13014:
																					{
																						return 30;
																					}
																					case 13015:
																					{
																						return 30;
																					}
																					case 13016:
																					{
																						return 30;
																					}
																					case 13017:
																					{
																						return 100;
																					}
																					case 13018:
																					{
																						return 100;
																					}
																					default:
																					{
																						switch (num)
																						{
																							case 9000:
																							{
																								return 100;
																							}
																							case 9001:
																							{
																								return 100;
																							}
																							case 9002:
																							{
																								return 150;
																							}
																							case 9003:
																							{
																								return 125;
																							}
																							case 9004:
																							{
																								return 175;
																							}
																							case 9005:
																							{
																								return 100;
																							}
																							case 9006:
																							{
																								return 100;
																							}
																							case 9007:
																							{
																								return 100;
																							}
																							case 9008:
																							{
																								return 100;
																							}
																							case 9009:
																							{
																								return 200;
																							}
																							case 9010:
																							{
																								return 150;
																							}
																							case 9011:
																							{
																								return 100;
																							}
																							case 9012:
																							{
																								return 100;
																							}
																							case 9013:
																							{
																								return 100;
																							}
																							default:
																							{
																								switch (num)
																								{
																									case 10000:
																									{
																										return 500;
																									}
																									case 10001:
																									{
																										return 800;
																									}
																									case 10002:
																									{
																										return 300;
																									}
																									case 10003:
																									{
																										return 200;
																									}
																									case 10004:
																									{
																										return 300;
																									}
																									case 10005:
																									{
																										return 300;
																									}
																									case 10006:
																									{
																										return 500;
																									}
																									case 10007:
																									{
																										return 800;
																									}
																									case 10008:
																									{
																										return 300;
																									}
																									case 10009:
																									{
																										return 400;
																									}
																									case 10010:
																									{
																										return 350;
																									}
																									case 10011:
																									{
																										return 450;
																									}
																									case 10012:
																									{
																										return 350;
																									}
																									case 10013:
																									{
																										return 400;
																									}
																									default:
																									{
																										switch (num)
																										{
																											case 15000:
																											{
																												return 200;
																											}
																											case 15001:
																											{
																												return 130;
																											}
																											case 15002:
																											{
																												return 50;
																											}
																											case 15003:
																											{
																												return 400;
																											}
																											case 15004:
																											{
																												return 250;
																											}
																											case 15005:
																											{
																												return 300;
																											}
																											case 15006:
																											{
																												return 300;
																											}
																											case 15007:
																											{
																												return 200;
																											}
																											case 15008:
																											{
																												return 300;
																											}
																											case 15009:
																											{
																												return 300;
																											}
																											default:
																											{
																												switch (num)
																												{
																													case 23000:
																													{
																														return 100;
																													}
																													case 23001:
																													{
																														return 75;
																													}
																													case 23002:
																													{
																														return 75;
																													}
																													case 23003:
																													{
																														return 75;
																													}
																													case 23004:
																													{
																														return 75;
																													}
																													case 23005:
																													{
																														return 75;
																													}
																													case 23006:
																													{
																														return 75;
																													}
																													case 23007:
																													{
																														return 150;
																													}
																													case 23008:
																													{
																														return 125;
																													}
																													default:
																													{
																														switch (num)
																														{
																															case 19000:
																															{
																																return 200;
																															}
																															case 19001:
																															{
																																return 150;
																															}
																															case 19002:
																															{
																																return 250;
																															}
																															case 19003:
																															{
																																return 250;
																															}
																															case 19004:
																															{
																																return 325;
																															}
																															case 19005:
																															{
																																return 400;
																															}
																															case 19006:
																															{
																																return 400;
																															}
																															case 19007:
																															{
																																return 400;
																															}
																															default:
																															{
																																switch (num)
																																{
																																	case 2000:
																																	{
																																		return 0;
																																	}
																																	case 2001:
																																	{
																																		return 0;
																																	}
																																	case 2002:
																																	{
																																		return 0;
																																	}
																																	case 2003:
																																	{
																																		return 0;
																																	}
																																	case 2004:
																																	{
																																		return 0;
																																	}
																																	case 2005:
																																	{
																																		return 0;
																																	}
																																	case 2006:
																																	{
																																		return 0;
																																	}
																																	default:
																																	{
																																		switch (num)
																																		{
																																			case 3000:
																																			{
																																				return 4500;
																																			}
																																			case 3001:
																																			{
																																				return 5000;
																																			}
																																			case 3002:
																																			{
																																				return 5000;
																																			}
																																			case 3003:
																																			{
																																				return 4000;
																																			}
																																			case 3004:
																																			{
																																				return 200;
																																			}
																																			case 3005:
																																			{
																																				return 150;
																																			}
																																			default:
																																			{
																																				switch (num)
																																				{
																																					case 11000:
																																					{
																																						return 100;
																																					}
																																					case 11001:
																																					{
																																						return 100;
																																					}
																																					case 11002:
																																					{
																																						return 100;
																																					}
																																					case 11003:
																																					{
																																						return 100;
																																					}
																																					case 11004:
																																					{
																																						return 150;
																																					}
																																					case 11005:
																																					{
																																						return 200;
																																					}
																																					default:
																																					{
																																						switch (num)
																																						{
																																							case 22000:
																																							{
																																								return 25;
																																							}
																																							case 22001:
																																							{
																																								return 30;
																																							}
																																							case 22002:
																																							{
																																								return 30;
																																							}
																																							case 22003:
																																							{
																																								return 30;
																																							}
																																							case 22004:
																																							{
																																								return 20;
																																							}
																																							default:
																																							{
																																								switch (num)
																																								{
																																									case 12000:
																																									{
																																										return 100;
																																									}
																																									case 12001:
																																									{
																																										return 75;
																																									}
																																									case 12002:
																																									{
																																										return 125;
																																									}
																																									default:
																																									{
																																										switch (num)
																																										{
																																											case 25000:
																																											{
																																												return 50;
																																											}
																																											case 25001:
																																											{
																																												return 20;
																																											}
																																											case 25002:
																																											{
																																												return 50;
																																											}
																																											default:
																																											{
																																												if (num != 20000)
																																												{
																																													if (num == 20001)
																																													{
																																														return 500;
																																													}
																																													if (num == 21000)
																																													{
																																														return 300;
																																													}
																																													if (num == 21001)
																																													{
																																														return 600;
																																													}
																																													if (num == 24000)
																																													{
																																														return 150;
																																													}
																																													if (num == 26000)
																																													{
																																														return 250;
																																													}
																																													if (num == 27000)
																																													{
																																														return 150;
																																													}
																																													if (num == 28000)
																																													{
																																														return 50;
																																													}
																																													return -1000;
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
		return 700;
	}
}