﻿using Infusion.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infusion.Clients
{
    internal class ClassicClientBehavior
    {
        public virtual void RegisterPackets()
        {
            PacketDefinitionRegistry.Register(PacketDefinitions.AddItemToContainer);
            PacketDefinitionRegistry.Register(PacketDefinitions.AttackRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.CharMoveRejection);
            PacketDefinitionRegistry.Register(PacketDefinitions.CharacterLocaleAndBody);
            PacketDefinitionRegistry.Register(PacketDefinitions.CharacterMoveAck);
            PacketDefinitionRegistry.Register(PacketDefinitions.CharactersStartingLocations);
            PacketDefinitionRegistry.Register(PacketDefinitions.ClientSpy);
            PacketDefinitionRegistry.Register(PacketDefinitions.ClientVersion);
            PacketDefinitionRegistry.Register(PacketDefinitions.ClientViewRange);
            PacketDefinitionRegistry.Register(PacketDefinitions.CreateCharacterRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.Damage);
            PacketDefinitionRegistry.Register(PacketDefinitions.DeleteObject);
            PacketDefinitionRegistry.Register(PacketDefinitions.DisconnectNotification);
            PacketDefinitionRegistry.Register(PacketDefinitions.DraggingOfItem);
            PacketDefinitionRegistry.Register(PacketDefinitions.DrawContainer);
            PacketDefinitionRegistry.Register(PacketDefinitions.DrawGamePlayer);
            PacketDefinitionRegistry.Register(PacketDefinitions.DrawObject);
            PacketDefinitionRegistry.Register(PacketDefinitions.EnableLockedClientFeatures);
            PacketDefinitionRegistry.Register(PacketDefinitions.Explosion);
            PacketDefinitionRegistry.Register(PacketDefinitions.GameServerList);
            PacketDefinitionRegistry.Register(PacketDefinitions.GameServerLoginRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.GeneralInformationPacket);
            PacketDefinitionRegistry.Register(PacketDefinitions.GetClientStatus);
            PacketDefinitionRegistry.Register(PacketDefinitions.GodModeRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.GraphicalEffect);
            PacketDefinitionRegistry.Register(PacketDefinitions.HealthBarStatusUpdate);
            PacketDefinitionRegistry.Register(PacketDefinitions.KickPlayer);
            PacketDefinitionRegistry.Register(PacketDefinitions.LoginSeed);
            PacketDefinitionRegistry.Register(PacketDefinitions.LoginCharacter);
            PacketDefinitionRegistry.Register(PacketDefinitions.LoginComplete);
            PacketDefinitionRegistry.Register(PacketDefinitions.LoginDenied);
            PacketDefinitionRegistry.Register(PacketDefinitions.LoginRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.MoveRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.NewHealthBarStatusUpdate);
            PacketDefinitionRegistry.Register(PacketDefinitions.ObjectInfo);
            PacketDefinitionRegistry.Register(PacketDefinitions.PauseClient);
            PacketDefinitionRegistry.Register(PacketDefinitions.PingMessage);
            PacketDefinitionRegistry.Register(PacketDefinitions.PlaySoundEffect);
            PacketDefinitionRegistry.Register(PacketDefinitions.RejectMoveItemRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.RequestWarMode);
            PacketDefinitionRegistry.Register(PacketDefinitions.SelectServerRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.SendSkills);
            PacketDefinitionRegistry.Register(PacketDefinitions.SendSpeech);
            PacketDefinitionRegistry.Register(PacketDefinitions.SpeechMessage);
            PacketDefinitionRegistry.Register(PacketDefinitions.SpeechRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.StatusBarInfo);
            PacketDefinitionRegistry.Register(PacketDefinitions.TalkRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.TipNoticeWindow);
            PacketDefinitionRegistry.Register(PacketDefinitions.ConnectToGameServer);
            PacketDefinitionRegistry.Register(PacketDefinitions.DropItemFailed);
            PacketDefinitionRegistry.Register(PacketDefinitions.DropItemApproved);
            PacketDefinitionRegistry.Register(PacketDefinitions.Blood);
            PacketDefinitionRegistry.Register(PacketDefinitions.MobAttributes);
            PacketDefinitionRegistry.Register(PacketDefinitions.WornItem);
            PacketDefinitionRegistry.Register(PacketDefinitions.FightOccuring);
            PacketDefinitionRegistry.Register(PacketDefinitions.AttackOk);
            PacketDefinitionRegistry.Register(PacketDefinitions.AttackEnded);
            PacketDefinitionRegistry.Register(PacketDefinitions.AddMultipleItemsInContainer);
            PacketDefinitionRegistry.Register(PacketDefinitions.PersonalLightLevel);
            PacketDefinitionRegistry.Register(PacketDefinitions.OverallLightLevel);
            PacketDefinitionRegistry.Register(PacketDefinitions.RejectCharacterLogon);
            PacketDefinitionRegistry.Register(PacketDefinitions.Time);
            PacketDefinitionRegistry.Register(PacketDefinitions.SetWeather);
            PacketDefinitionRegistry.Register(PacketDefinitions.PlayMidiMusic);
            PacketDefinitionRegistry.Register(PacketDefinitions.CharacterAnimation);
            PacketDefinitionRegistry.Register(PacketDefinitions.OpenBuyWindow);
            PacketDefinitionRegistry.Register(PacketDefinitions.NewSubserver);
            PacketDefinitionRegistry.Register(PacketDefinitions.UpdatePlayer);
            PacketDefinitionRegistry.Register(PacketDefinitions.OpenDialogBox);
            PacketDefinitionRegistry.Register(PacketDefinitions.ResendCharactersAfterDelete);
            PacketDefinitionRegistry.Register(PacketDefinitions.OpenPaperdoll);
            PacketDefinitionRegistry.Register(PacketDefinitions.CorpseClothing);
            PacketDefinitionRegistry.Register(PacketDefinitions.DisplaySign);
            PacketDefinitionRegistry.Register(PacketDefinitions.MapMessage);
            PacketDefinitionRegistry.Register(PacketDefinitions.MovePlayer);
            PacketDefinitionRegistry.Register(PacketDefinitions.RequestAssistance);
            PacketDefinitionRegistry.Register(PacketDefinitions.SellList);
            PacketDefinitionRegistry.Register(PacketDefinitions.UpdateCurrentHealth);
            PacketDefinitionRegistry.Register(PacketDefinitions.UpdateCurrentMana);
            PacketDefinitionRegistry.Register(PacketDefinitions.UpdateCurrentStamina);
            PacketDefinitionRegistry.Register(PacketDefinitions.OpenWebBrowser);
            PacketDefinitionRegistry.Register(PacketDefinitions.AllowRefuseAttack);
            PacketDefinitionRegistry.Register(PacketDefinitions.GumpTextEntryDialog);
            PacketDefinitionRegistry.Register(PacketDefinitions.DisplayDeathAction);
            PacketDefinitionRegistry.Register(PacketDefinitions.SendGumpMenuDialog);
            PacketDefinitionRegistry.Register(PacketDefinitions.ChatMessage);
            PacketDefinitionRegistry.Register(PacketDefinitions.HelpTileData);
            PacketDefinitionRegistry.Register(PacketDefinitions.QuestArrow);
            PacketDefinitionRegistry.Register(PacketDefinitions.SeasonalInformation);
            PacketDefinitionRegistry.Register(PacketDefinitions.GraphicalEffect2);
            PacketDefinitionRegistry.Register(PacketDefinitions.ClilocMessage);
            PacketDefinitionRegistry.Register(PacketDefinitions.Semivisible);
            PacketDefinitionRegistry.Register(PacketDefinitions.InvalidMapEnable);
            PacketDefinitionRegistry.Register(PacketDefinitions.GlobalQueueCount);
            PacketDefinitionRegistry.Register(PacketDefinitions.ClilocMessageAffix);
            PacketDefinitionRegistry.Register(PacketDefinitions.ExtendedDrawGamePlayer);
            PacketDefinitionRegistry.Register(PacketDefinitions.ExtendedDrawObject);
            PacketDefinitionRegistry.Register(PacketDefinitions.SendCustomHouse);
            PacketDefinitionRegistry.Register(PacketDefinitions.CharacterTransferLog);
            PacketDefinitionRegistry.Register(PacketDefinitions.SecondAgeRevision);
            PacketDefinitionRegistry.Register(PacketDefinitions.CompressedGump);
            PacketDefinitionRegistry.Register(PacketDefinitions.UpdateMobileStatus);
            PacketDefinitionRegistry.Register(PacketDefinitions.BuffSystem);
            PacketDefinitionRegistry.Register(PacketDefinitions.NewCharacterAnimation);
            PacketDefinitionRegistry.Register(PacketDefinitions.KrEncryptionResponse);
            PacketDefinitionRegistry.Register(PacketDefinitions.SecondAgeObjectInformation);
            PacketDefinitionRegistry.Register(PacketDefinitions.NewMapMessage);
            PacketDefinitionRegistry.Register(PacketDefinitions.DoubleClick);
            PacketDefinitionRegistry.Register(PacketDefinitions.PickUpItem);
            PacketDefinitionRegistry.Register(PacketDefinitions.DropItem);
            PacketDefinitionRegistry.Register(PacketDefinitions.SingleClick);
            PacketDefinitionRegistry.Register(PacketDefinitions.RequestSkills);
            PacketDefinitionRegistry.Register(PacketDefinitions.DropWearItem);
            PacketDefinitionRegistry.Register(PacketDefinitions.ControlAnimation);
            PacketDefinitionRegistry.Register(PacketDefinitions.PathfinidingInClient);
            PacketDefinitionRegistry.Register(PacketDefinitions.BuyItems);
            PacketDefinitionRegistry.Register(PacketDefinitions.VersionOk);
            PacketDefinitionRegistry.Register(PacketDefinitions.NewArtwork);
            PacketDefinitionRegistry.Register(PacketDefinitions.NewTerrain);
            PacketDefinitionRegistry.Register(PacketDefinitions.NewAnimation);
            PacketDefinitionRegistry.Register(PacketDefinitions.NewHues);
            PacketDefinitionRegistry.Register(PacketDefinitions.DeleteArt);
            PacketDefinitionRegistry.Register(PacketDefinitions.CheckClientVersion);
            PacketDefinitionRegistry.Register(PacketDefinitions.ScriptNames);
            PacketDefinitionRegistry.Register(PacketDefinitions.EditScriptFile);
            PacketDefinitionRegistry.Register(PacketDefinitions.BoardHeader);
            PacketDefinitionRegistry.Register(PacketDefinitions.BoardMessage);
            PacketDefinitionRegistry.Register(PacketDefinitions.BoardPostMessage);
            PacketDefinitionRegistry.Register(PacketDefinitions.UpdateRegions);
            PacketDefinitionRegistry.Register(PacketDefinitions.AddRegion);
            PacketDefinitionRegistry.Register(PacketDefinitions.NewContextFx);
            PacketDefinitionRegistry.Register(PacketDefinitions.UpdateContextFx);
            PacketDefinitionRegistry.Register(PacketDefinitions.RestartVersion);
            PacketDefinitionRegistry.Register(PacketDefinitions.ServerListing);
            PacketDefinitionRegistry.Register(PacketDefinitions.ServerListAddEntry);
            PacketDefinitionRegistry.Register(PacketDefinitions.ServerListRemoveEntry);
            PacketDefinitionRegistry.Register(PacketDefinitions.RemoveStaticObject);
            PacketDefinitionRegistry.Register(PacketDefinitions.MoveStaticObject);
            PacketDefinitionRegistry.Register(PacketDefinitions.LoadArea);
            PacketDefinitionRegistry.Register(PacketDefinitions.LoadAreaRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.ChangeTextColor);
            PacketDefinitionRegistry.Register(PacketDefinitions.RenameCharacter);
            PacketDefinitionRegistry.Register(PacketDefinitions.ResponseToDialogBox);
            PacketDefinitionRegistry.Register(PacketDefinitions.DeleteCharacter);
            PacketDefinitionRegistry.Register(PacketDefinitions.CharacterCreation);
            PacketDefinitionRegistry.Register(PacketDefinitions.RequestHelp);
            PacketDefinitionRegistry.Register(PacketDefinitions.SellListReply);
            PacketDefinitionRegistry.Register(PacketDefinitions.RequestTipNoticeWindow);
            PacketDefinitionRegistry.Register(PacketDefinitions.GumpTextEntryDialogReply);
            PacketDefinitionRegistry.Register(PacketDefinitions.GumpMenuSelection);
            PacketDefinitionRegistry.Register(PacketDefinitions.ChatText);
            PacketDefinitionRegistry.Register(PacketDefinitions.OpenChatWindow);
            PacketDefinitionRegistry.Register(PacketDefinitions.SendHelpTipRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.InvalidMapRequest);
            PacketDefinitionRegistry.Register(PacketDefinitions.ParticleEffect);
            PacketDefinitionRegistry.Register(PacketDefinitions.SpyOnClient);
            PacketDefinitionRegistry.Register(PacketDefinitions.Follow);
            PacketDefinitionRegistry.Register(PacketDefinitions.ResurrectionMenu);
            PacketDefinitionRegistry.Register(PacketDefinitions.RemoveGroup);
            PacketDefinitionRegistry.Register(PacketDefinitions.MapPacket);
            PacketDefinitionRegistry.Register(PacketDefinitions.BooksPages);
            PacketDefinitionRegistry.Register(PacketDefinitions.TargetCursor);
            PacketDefinitionRegistry.Register(PacketDefinitions.SecureTraiding);
            PacketDefinitionRegistry.Register(PacketDefinitions.BulletinBoardMessage);
            PacketDefinitionRegistry.Register(PacketDefinitions.BookHeader);
            PacketDefinitionRegistry.Register(PacketDefinitions.DyeWindow);
            PacketDefinitionRegistry.Register(PacketDefinitions.GiveBoatHousePlacementView);
            PacketDefinitionRegistry.Register(PacketDefinitions.ConsoleEntryPrompt);
            PacketDefinitionRegistry.Register(PacketDefinitions.RequestCharProfile);
            PacketDefinitionRegistry.Register(PacketDefinitions.UltimaMessenger);
            PacketDefinitionRegistry.Register(PacketDefinitions.AssistVersion);
            PacketDefinitionRegistry.Register(PacketDefinitions.UnicodeTextEntry);
            PacketDefinitionRegistry.Register(PacketDefinitions.ConfigurationFile);
            PacketDefinitionRegistry.Register(PacketDefinitions.LogoutStatus);
            PacketDefinitionRegistry.Register(PacketDefinitions.BookHeaderNew);
            PacketDefinitionRegistry.Register(PacketDefinitions.MegaCliloc);
            PacketDefinitionRegistry.Register(PacketDefinitions.GenericAosCommands);
            PacketDefinitionRegistry.Register(PacketDefinitions.KrriosClientSpecial);
            PacketDefinitionRegistry.Register(PacketDefinitions.FreeShardList);
        }
    }

    internal class ClassicClientBehaviorSince60142 : ClassicClientBehavior
    {
        public override void RegisterPackets()
        {
            base.RegisterPackets();

            PacketDefinitionRegistry.Register(PacketDefinitions.EnableLockedClientFeaturesSince6_0_14_2);
        }
    }

    internal class ClassicClientBehaviorSince7000 : ClassicClientBehaviorSince60142
    {
        public override void RegisterPackets()
        {
            base.RegisterPackets();

            PacketDefinitionRegistry.Register(PacketDefinitions.DrawObject7000);
        }
    }

    internal class ClassicClientBehaviorSince7090 : ClassicClientBehaviorSince7000
    {
        public override void RegisterPackets()
        {
            base.RegisterPackets();

            PacketDefinitionRegistry.Register(PacketDefinitions.DrawContainer7090);
        }
    }
}
