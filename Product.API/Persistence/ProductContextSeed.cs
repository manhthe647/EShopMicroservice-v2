using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Serilog;

namespace Product.API.Persistence
{
    public class ProductContextSeed
    {
        public static async Task SeedAsync(ProductContext context)
        {
            if (await context.Products.AnyAsync())
            {
                Log.Information("Products already exist in database. Skipping seed data.");
                return;
            }

            Log.Information("Seeding Yu-Gi-Oh card data...");

            var products = new List<Entities.CardProduct>
            {
                new Entities.CardProduct
                {
                    No = "YGO-001",
                    Name = "Blue-Eyes White Dragon",
                    Description = "LIGHT Dragon Normal Monster - Level 8 - ATK: 3000 / DEF: 2500. This legendary dragon is a powerful engine of destruction. Virtually invincible, very few have faced this awesome creature and lived to tell the tale.",
                    Price = 250000,
                    ImageUrl = "https://bizweb.dktcdn.net/100/429/539/products/45f182573c1dd9df848ad24ff20419dc-tn.jpg?v=1628046724797",
                    Summary = "Legendary white dragon with overwhelming power",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-002",
                    Name = "Dark Magician",
                    Description = "DARK Spellcaster Normal Monster - Level 7 - ATK: 2500 / DEF: 2100. The ultimate wizard in terms of attack and defense.",
                    Price = 180000,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSxJeNwscetEKWXaFkckqIbLYoSuJm-7JGy7A&s",
                    Summary = "The ultimate wizard with balanced attack and defense",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-003",
                    Name = "Exodia the Forbidden One",
                    Description = "DARK Spellcaster Effect Monster - Level 3 - ATK: 1000 / DEF: 1000. If you have 'Right Leg of the Forbidden One', 'Left Leg of the Forbidden One', 'Right Arm of the Forbidden One' and 'Left Arm of the Forbidden One' in addition to this card in your hand, you win the Duel.",
                    Price = 1500000,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRC8anLD_eR5b6vpe4Pw68YBjTH1JccTHH4fQ&s",
                    Summary = "Forbidden creature that grants instant victory",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-004",
                    Name = "Red-Eyes Black Dragon",
                    Description = "DARK Dragon Normal Monster - Level 7 - ATK: 2400 / DEF: 2000. A ferocious dragon with a deadly attack.",
                    Price = 220000,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQc7r0pzfEWpYPsf08HtvYcDYmPdsZB79XDlA&s",
                    Summary = "Ferocious black dragon with burning crimson eyes",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-005",
                    Name = "Mirror Force",
                    Description = "Normal Trap Card. When an opponent's monster declares an attack: Destroy all Attack Position monsters your opponent controls.",
                    Price = 85000,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQzA45tBcABufAnTpsSTIKR3oX4W_jQVLysyw&s",
                    Summary = "Devastating trap that destroys all attacking monsters",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-006",
                    Name = "Pot of Greed",
                    Description = "Normal Spell Card. Draw 2 cards.",
                    Price = 95000,
                    ImageUrl = "https://bizweb.dktcdn.net/100/429/539/products/pot-of-greed-ddaf8703-c1c3-45a5-b70d-6de7f83a1139.jpg?v=1627194048737",
                    Summary = "Forbidden spell that allows drawing 2 cards",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-007",
                    Name = "Elemental HERO Sparkman",
                    Description = "LIGHT Warrior Normal Monster - Level 4 - ATK: 1600 / DEF: 1400. When this card is Normal Summoned, you can add 1 'Polymerization' from your Deck to your hand.",
                    Price = 45000,
                    ImageUrl = "https://static.wikia.nocookie.net/yugioh/images/e/e2/ElementalHEROSparkman-SGX2-EN-C-1E.png/revision/latest?cb=20221105021720",
                    Summary = "Electric hero with fusion support abilities",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-008",
                    Name = "Mystical Space Typhoon",
                    Description = "Quick-Play Spell Card. Target 1 Spell/Trap on the field; destroy it.",
                    Price = 35000,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSlKkZ-LKi5UaaOEpDYLpgfoiFqU3xbBBbrjw&s",
                    Summary = "Quick spell for destroying Spell/Trap cards",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-009",
                    Name = "Kuriboh",
                    Description = "DARK Fiend Effect Monster - Level 1 - ATK: 300 / DEF: 200. During your opponent's Battle Phase, at damage calculation: You can discard this card; you take no battle damage from that battle.",
                    Price = 25000,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/71FjcOce1dL.jpg",
                    Summary = "Cute furry creature that prevents battle damage",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-010",
                    Name = "Sangan",
                    Description = "DARK Fiend Effect Monster - Level 3 - ATK: 1000 / DEF: 600. If this card is sent from the field to the Graveyard: Add 1 monster with 1500 or less ATK from your Deck to your hand.",
                    Price = 55000,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRWQ-IRMA4bAv-JNFfA3BBhlagykndUj4GU2w&s",
                    Summary = "Creepy searcher that finds weak monsters",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-011",
                    Name = "Black Luster Soldier",
                    Description = "EARTH Warrior Ritual Monster - Level 8 - ATK: 3000 / DEF: 2500. A legendary warrior whose combat skills are second to none. This monster can only be Ritual Summoned with the Ritual Spell Card, 'Black Luster Ritual'.",
                    Price = 320000,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTi046gRftTqVxvhPmII2QeHEtjUgOdrSUDTw&s",
                    Summary = "Legendary ritual warrior with supreme combat skills",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-012",
                    Name = "Celtic Guardian",
                    Description = "EARTH Warrior Normal Monster - Level 4 - ATK: 1400 / DEF: 1200. An elf who learned to wield a sword, he baffles enemies with lightning-swift attacks.",
                    Price = 15000,
                    ImageUrl = "https://example.com/cards/celtic-guardian.jpg",
                    Summary = "Swift elf warrior with lightning-fast attacks",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-013",
                    Name = "Jinzo",
                    Description = "DARK Machine Effect Monster - Level 6 - ATK: 2400 / DEF: 1500. Trap Cards cannot be activated. Negate all Trap effects on the field.",
                    Price = 275000,
                    ImageUrl = "https://example.com/cards/jinzo.jpg",
                    Summary = "Cybernetic being that negates all trap cards",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-014",
                    Name = "Summoned Skull",
                    Description = "DARK Fiend Normal Monster - Level 6 - ATK: 2500 / DEF: 1200. A fiend with dark powers for confusing enemies. Among the Fiend-Type monsters, this monster boasts considerable force.",
                    Price = 120000,
                    ImageUrl = "https://example.com/cards/summoned-skull.jpg",
                    Summary = "Powerful fiend with dark confusing powers",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-015",
                    Name = "Raigeki",
                    Description = "Normal Spell Card. Destroy all monsters your opponent controls.",
                    Price = 450000,
                    ImageUrl = "https://example.com/cards/raigeki.jpg",
                    Summary = "Lightning spell that destroys all enemy monsters",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-016",
                    Name = "Flame Swordsman",
                    Description = "FIRE Warrior Fusion Monster - Level 5 - ATK: 1800 / DEF: 1600. 'Flame Manipulator' + 'Elemental HERO Sparkman'. A warrior enveloped in flames. Few can match his swordsmanship.",
                    Price = 75000,
                    ImageUrl = "https://example.com/cards/flame-swordsman.jpg",
                    Summary = "Fusion warrior wielding flaming sword techniques",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-017",
                    Name = "Magician of Black Chaos",
                    Description = "DARK Spellcaster Ritual Monster - Level 8 - ATK: 2800 / DEF: 2600. A master of black magic that is summoned using black magic ritual. This monster's magical powers can overcome any enemy.",
                    Price = 380000,
                    ImageUrl = "https://example.com/cards/magician-black-chaos.jpg",
                    Summary = "Dark ritual magician with overwhelming magical power",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-018",
                    Name = "Fissure",
                    Description = "Normal Spell Card. Target 1 face-up monster your opponent controls with the lowest ATK (your choice, if tied); destroy that target.",
                    Price = 20000,
                    ImageUrl = "https://example.com/cards/fissure.jpg",
                    Summary = "Earth-splitting spell that destroys the weakest monster",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-019",
                    Name = "Time Wizard",
                    Description = "LIGHT Spellcaster Effect Monster - Level 2 - ATK: 500 / DEF: 400. Once per turn: You can toss a coin and call it. If you call it right, destroy all monsters on your opponent's field. If you call it wrong, destroy all monsters on your field, and take damage equal to half your LP.",
                    Price = 85000,
                    ImageUrl = "https://example.com/cards/time-wizard.jpg",
                    Summary = "Gambling wizard that controls the flow of time",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Entities.CardProduct
                {
                    No = "YGO-020",
                    Name = "Thousand-Eyes Restrict",
                    Description = "DARK Spellcaster Fusion Monster - Level 1 - ATK: 0 / DEF: 0. 'Thousand-Eyes Idol' + 'Relinquished'. Cannot attack. Other monsters on the field cannot change their battle positions or attack. Once per turn: You can target 1 monster your opponent controls; equip that target to this card.",
                    Price = 650000,
                    ImageUrl = "https://example.com/cards/thousand-eyes-restrict.jpg",
                    Summary = "Forbidden fusion that controls the entire battlefield",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "System"
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();

            Log.Information("Seeded {Count} Yu-Gi-Oh cards successfully", products.Count);
        }
    }
}