/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Acrolinx.Sdk.Sidebar.Util.Changetracking;
using Acrolinx.Sdk.Sidebar.Documents;
using System.Diagnostics;

namespace Acrolinx.Sdk.Sidebar.Tests
{
    [TestClass]
    public class LookupTest
    {
        [TestMethod]
        public void SimpleSearch()
        {
            var lookup = new Lookup("This is an test");
            var range = new Range(0, "This".Length);
            var result = lookup.Search("This is an test", range);

            Assert.AreEqual(result[0], range);
        }
        
        [TestMethod]
        public void TextHasMoved()
        {
            var lookup = new Lookup("This is an test");
            var range = new Range(0, "This".Length);
            var result = lookup.Search("AA This is an test", range);

            Assert.AreEqual(result[0], new Range("AA ".Length, "AA This".Length));
        }

        [TestMethod]
        public void MultiRangeTextHasMoved()
        {
            var lookup = new Lookup("This is an test");
            var range1 = new Range(0, "This".Length);
            var range2 = new Range("This ".Length, "This is".Length);
            var result = lookup.Search("AA This is an test", range1, range2);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range("AA ".Length, "AA This".Length));
            Assert.AreEqual(result[1], new Range("AA This ".Length, "AA This is".Length));
        }

        [TestMethod]
        public void MultiTextFoundAtCorrectPosition()
        {
            var lookup = new Lookup("A A");
            var range1 = new Range(0, 1);

            var result = lookup.Search("A A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(0, 1));
        }

        [TestMethod]
        public void MultiTextFoundAtCorrectPosition2()
        {
            var lookup = new Lookup("A A");
            var range1 = new Range(2, 3);

            var result = lookup.Search("A A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(2, 3));
        }

        [TestMethod]
        public void MultiTextFoundAtCorrectPosition3()
        {
            var lookup = new Lookup("A A");
            var range1 = new Range(0, 1);

            var result = lookup.Search(" A A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(1, 2));
        }

        [TestMethod]
        public void MultiTextFoundAtCorrectPosition4()
        {
            var lookup = new Lookup("A A");
            var range1 = new Range(2, 3);

            var result = lookup.Search(" A A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(3, 4));
        }

        [TestMethod]
        public void PartOfTextIsFound()
        {
            var lookup = new Lookup("AB");
            var range1 = new Range(1, 2);

            var result = lookup.Search(" AB", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(2, 3));
        }

        [TestMethod]
        public void PartOfTextIsFound2()
        {
            var lookup = new Lookup("ABCD");
            var range1 = new Range(1, 3);

            var result = lookup.Search(" ABCD", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(2, 4));
        }
        
        [TestMethod]
        public void TextFoundIfOriginalDeleted()
        {
            var lookup = new Lookup("A A");
            var range1 = new Range(2, 3);

            var result = lookup.Search("A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(0, 1));
        }

        [TestMethod]
        public void TextFoundIfOriginalDeleted2()
        {
            var lookup = new Lookup("A A");
            var range1 = new Range(0, 1);

            var result = lookup.Search("  A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(2, 3));
        }

        [TestMethod]
        public void TextFoundIfOriginalDeleted3()
        {
            var lookup = new Lookup("A A A B");
            var range1 = new Range(5, 6);

            var result = lookup.Search("  A A B", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(5, 6));
        }

        [TestMethod]
        public void ChangedTextIsFound()
        {
            var lookup = new Lookup("This is an test");
            var range = new Range(0, "This".Length);
            var result = lookup.Search("AAThis is an test", range);

            Assert.AreEqual(result[0], new Range(2, 6));
        }

        [TestMethod]
        public void PartContainingXMLSimple()
        {
            var lookup = new Lookup("<x>a<b>c");
            var ranges = new Range[] { new Range(7, 8)};

            var result = lookup.Search("<x> a<b>c", ranges);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(8,9));
        }

        [TestMethod]
        public void PartContainingXMLandNl()
        {
            var lookup = new Lookup("<x>\n<b>c");
            var ranges = new Range[] { new Range(4, 5), new Range(5, 6), new Range(6, 7), new Range(7, 8) };

            var result = lookup.Search("<x>a\n<b>c", ranges);

            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(result[0], new Range(5, 6));
        }

        [TestMethod]
        public void PartContainingXMLandNl2()
        {
            var lookup = new Lookup("<x>\r\n<b>c");
            var ranges = new Range[] { new Range(5, 6), new Range(6, 7), new Range(7, 8), new Range(8, 9) };

            var result = lookup.Search("<x>a\r\n<b>c", ranges);

            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(result[0], new Range(6, 7));
        }

        [TestMethod]
        public void PartContainingXMLSelectingNl()
        {
            var lookup = new Lookup("<x>\r\n<b>\r\n");
            var ranges = new Range[] { new Range(8, 9), new Range(9, 10)};

            var result = lookup.Search("<x>a\r\n<b>\r\n", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range(9, 10));
        }

        [TestMethod]
        public void PartContainingXML()
        {
            var lookup = new Lookup("<x>\r\n    <some>Tesst</some>");
            var ranges = new Range[] { new Range(15, 20)};

            var result = lookup.Search("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
        }

        [TestMethod]
        public void PartContainingXML2()
        {
            var lookup = new Lookup("<x>\r\n    <some>Tesst</some>\r\n    ");
            var ranges = new Range[] { new Range(15, 20), new Range(27, 29) };

            var result = lookup.Search("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
            Assert.AreEqual(result[1], new Range(28, 30));
        }

        [TestMethod]
        public void PartContainingXML2Simple()
        {
            var lookup = new Lookup("<x>\r\n<s>Tesst</s>\r\n");
            var ranges = new Range[] { new Range(8, 13), new Range(17, 19) };

            var result = lookup.Search("<x>a\r\n<s>Tesst</s>\r\n", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range(9, 14));
            Assert.AreEqual(result[1], new Range(18, 20));
        }

        [TestMethod]
        public void PartContainingXML3()
        {
            var lookup = new Lookup("<x>\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>");
            var ranges = new Range[]{ new Range(15,20), new Range(27, 29),new Range(29, 30),new Range(30, 31), new Range(31, 32), new Range(32, 33), new Range(45, 49)};

            var result = lookup.Search("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(7, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
        }

        [TestMethod]
        public void PartContainingXML4()
        {
            var lookup = new Lookup("<x>\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>");
            var ranges = new Range[] { new Range(15, 20), new Range(30, 31), new Range(31, 32), new Range(32, 33), new Range(45, 49) };

            var result = lookup.Search("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(5, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
        }

        [TestMethod]
        public void PartContainingXML5()
        {
            var lookup = new Lookup("<x>\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>");
            var ranges = new Range[] { new Range(15, 20), new Range(45, 49) };

            var result = lookup.Search("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
        }

        [TestMethod]
        public void PerfromanceTestHTML()
        {
            string xml = "<root><that>native</that><!--slope--><difference><would><report putting=\"draw\">dead<![CDATA[tiny creature fly value whole]]></report><![CDATA[struck smallest corn]]><engineer><everyone><general>1122739026</general><hall><general>1327077737</general><ought>require</ought><fence><been trade=\"anything\">1809532956</been><on>origin</on><doubt><tomorrow><!--hole diameter plane faster ring muscle could-->1062367322.1040087</tomorrow><ten is=\"moment\">shadow<!--model service public driven that than yellow--></ten><play><chosen><graph ancient=\"soft\">taken</graph><!--substance as--><skill>236528695.06021786</skill><explain watch=\"east\">-1376853916</explain><!--listen travel hair farmer window--><leaving first=\"halfway\"><![CDATA[calm]]>130090471</leaving><!--already orange grow--><suppose><properly>protection</properly><!--studying importance send rear son carry various for dug sunlight--><pig business=\"bread\"><sunlight><struggle><![CDATA[funny comfortable]]>rear</struggle><characteristic><!--steam--><about beauty=\"unknown\">1273468858.0353642</about><movie topic=\"done\"><!--our anything explanation rich--><strike>1896621318</strike><snow coat=\"sunlight\"><gas>-110479583.49193096</gas><cow brass=\"duty\">-2092276389</cow><cry clean=\"correctly\">-1377299707</cry><watch><east><cotton dog=\"useful\">1405717987.8542633<![CDATA[wooden mission master made hurt doing dozen]]></cotton><chair>1976164192</chair><![CDATA[shore dinner lot]]><just ate=\"needed\">forest</just><ranch><equally becoming=\"policeman\"><brave mind=\"already\">post</brave><machinery>mainly</machinery><bark shallow=\"suggest\">other<!--direct apartment pour gravity force unusual sad carbon--></bark><yourself village=\"bear\">1127847599.885682</yourself><fact dull=\"fifteen\"><![CDATA[leaving machinery party care desk must]]><cannot><thing>902578978.1991644<!--famous broke--></thing><last boat=\"why\"><![CDATA[suddenly]]>control</last><![CDATA[method sound cotton book floating worth]]><grown>1968365931</grown><!--golden customs foot customs--><completely>blanket</completely><wish>cloth</wish><!--pitch make introduced--><hill><charge>-1804895529.4945512</charge><night><purple anybody=\"you\">-359101725</purple><![CDATA[flat]]><package fell=\"range\">-931341086</package><wagon acres=\"reader\">told</wagon><dig greatest=\"watch\"><![CDATA[join down short eight captured police crack spirit than camera log touch]]><exclaimed willing=\"thin\">2087375331.228341</exclaimed><establish mail=\"birds\"><satisfied><clothing>buy</clothing><political><occasionally forgot=\"setting\"><service>1749364113.0600753</service><whom><!--paint-->1220658321.8887482</whom><!--likely--><correctly><!--rope tiny-->library</correctly><parent hold=\"very\">felt<![CDATA[east national]]></parent><fear chain=\"left\">children</fear><purple>-1665854458</purple><terrible><clay><drew>302778185.3066845</drew><![CDATA[people made melted dress wild thread into proper]]><including some=\"fierce\">-1633916319</including><nearly strange=\"loss\">next<![CDATA[given]]></nearly><sent rough=\"warn\"><!--column wooden wore beauty park finger--><cave hard=\"plan\"><careful><care><!--hole offer for sell grow tomorrow plant quickly manner agree--><hurried>gulf</hurried><knew>men<![CDATA[fly bent directly mirror them browserling]]></knew><fair><enjoy idea=\"wing\"><![CDATA[particles create college tree]]><disappear>-1275094490</disappear><sudden lucky=\"arrangement\">-645745896.4873524</sudden><came bone=\"many\"><nor>steel</nor><sat rest=\"kill\"><captured telephone=\"suggest\">-23557113.8480134</captured><angry phrase=\"nine\">-483188206.0902972</angry><![CDATA[boat forty storm far gun recognize donkey]]><forgot why=\"properly\">-1439011160.7404099</forgot><!--account full--><indeed manner=\"win\"><sometime come=\"shine\"><!--chemical--><![CDATA[few already]]>34274528</sometime><form>forgotten</form><lips>sentence</lips><!--wait--><failed tail=\"saddle\">gave</failed><direction start=\"system\"><![CDATA[must shake observe]]>consonant<!--shadow among--></direction><![CDATA[pencil fuel]]><sudden search=\"unusual\">-1403838342</sudden><she growth=\"captured\">840705289.7410693</she><active>choose</active><paid carbon=\"get\">low</paid><![CDATA[note so]]><shade>-914530815.2269163</shade></indeed><heading regular=\"push\">-294116813.0500617</heading><everybody>-179262849.28649426</everybody><nails suit=\"shells\">-610724434<![CDATA[usual order must such four dance harbor hospital bad place]]></nails><melted die=\"base\">-2128657349</melted><![CDATA[gift in system pole]]><young>-1228738781</young><!--combination lips constantly tall near should fought doctor body begun--><buried engine=\"full\">335748403.44583035</buried></sat><!--situation whom began clothing--><leader nervous=\"earth\">1379909720</leader><believed>trip<!--giving--><![CDATA[circus bill]]></believed><species discovery=\"protection\"><!--tree-->-560759784.3650217</species><spite>1733674009</spite><hall easily=\"television\">249192283.64177227</hall><close>-972863952.0825939</close><!--planet flat--><aid>mainly</aid><when word=\"owner\">1770745699<!--promised pond--></when></came><saved support=\"circus\">-236073514</saved><present west=\"stand\">shallow</present><evidence>whatever</evidence><birth function=\"seed\">-794529331</birth><jack>week</jack><season sight=\"material\">spread</season><we take=\"somebody\">putting</we></enjoy><proud every=\"south\"><![CDATA[master]]>silver</proud><![CDATA[shut neighborhood]]><accept consonant=\"shaking\">tool</accept><!--every--><coming>seems</coming><fighting>natural</fighting><!--brass hat occasionally know fuel selection whistle myself average supply soil road suddenly does--><lying village=\"wonderful\">tiny</lying><bare date=\"fellow\"><![CDATA[applied joy kept keep symbol where]]>298586956.66218424</bare><social sheep=\"lack\">796602368.6119423</social><another lamp=\"outside\">compass</another><smell>1466913286</smell></fair><serious pen=\"claws\">-810502480<!--beauty saw percent dawn--></serious><old numeral=\"copper\">save</old><road pattern=\"silver\">1405338855.3827434<![CDATA[over]]></road><both><![CDATA[cup melted]]>1235320093.6041026</both><locate>1120915440.3421664</locate><rush task=\"cost\">slight</rush><farmer>note</farmer></care><buy beautiful=\"notice\">compound</buy><sides sound=\"kind\">1380902842.8673573</sides><audience allow=\"pool\">146048458</audience><wheat>stepped</wheat><desert common=\"threw\">size</desert><claws desk=\"pay\">join</claws><![CDATA[air surprise speed pale buried identity universe orbit invented]]><memory>call</memory><reader belong=\"herd\">slowly</reader><joy food=\"western\">instance</joy></careful><element>acrolinx</element><we verb=\"fireplace\">control</we><man>1774564338.463005</man><order>key</order><opinion>vertical</opinion><larger>cabin</larger><perhaps>summer<![CDATA[able]]></perhaps><though>motion<!--breeze find opposite additional hide per tea nuts--><![CDATA[crack]]></though><!--spoken lonely send develop before--><by sun=\"rapidly\">-1030021265.8956566</by></cave><twelve>1107853248.9693112</twelve><!--grade--><pick locate=\"safe\">evening</pick><donkey><!--tool-->blow</donkey><crowd>-1902237267.9245038</crowd><potatoes><![CDATA[fastened]]>-783790534.6696854<!--dear fighting satisfied mail pass hope dream hard foreign plate--></potatoes><wave>frozen<!--uncle--></wave><thought><!--chicken flow weak his-->city</thought><variety stranger=\"wood\">dead</variety><principle>husband</principle></sent><material>-1047418329.3654494</material><![CDATA[donkey eaten comfortable floor]]><right city=\"port\">balance</right><chance>342112045.10907316</chance><why cheese=\"give\">733004937<!--deeply function--><![CDATA[these current too pride cup specific]]></why><!--been age--><vegetable sentence=\"balloon\">-1745021008.7245693</vegetable><chair>or<!--lift gently event giving wheat--></chair></clay><control>166711762<!--hat--></control><law><!--list yes city door-->respect</law><offer wooden=\"against\">trace</offer><![CDATA[appropriate manner]]><!--truth--><organized grew=\"pole\">identity</organized><!--outline spite--><stepped>production</stepped><period>1502219335</period><noon>fireplace</noon><break>desert</break><remarkable house=\"island\">-1052005661</remarkable></terrible><![CDATA[relationship connected]]><syllable goose=\"fairly\">400864596.77425575</syllable><repeat muscle=\"difficult\">-1002073110</repeat><!--shirt--><boat previous=\"fire\">end</boat></occasionally><remember tonight=\"belt\">mirror</remember><doll>1058628805</doll><image chest=\"finest\">-880241975</image><closely quiet=\"outside\">1934047579</closely><bus>provide</bus><![CDATA[shall mean tent fifth arrange]]><mathematics whistle=\"spring\">-579012910</mathematics><arrange>shirt</arrange><!--handsome union--><![CDATA[either order]]><horn><!--pony-->-2090737490.0016646</horn><![CDATA[essential slip]]><behavior>program</behavior></political><walk citizen=\"like\">1304959769</walk><distant around=\"sang\">farmer</distant><![CDATA[before]]><connected loud=\"firm\">prevent<!--main--></connected><![CDATA[sent]]><beat ten=\"ants\">768235900.3594475</beat><!--burst--><amount leaving=\"off\">-1427784070</amount><![CDATA[weather interest customs everyone copper touch]]><cloud hand=\"settlers\">-379747704</cloud><gave story=\"primitive\">1118558505</gave><neck several=\"reach\">-1741694151</neck></satisfied><social><!--down layers report desk tongue pack-->994086412.0788212</social><![CDATA[wrapped being bear rest sleep very]]><on>shown<![CDATA[needed apart]]><!--donkey--></on><through sang=\"course\">1279233934.4998598</through><probably>forty</probably><equator>ring</equator><fight>experience</fight><safety>-1523264265.2210135<!--south wheel onlinetools skin--></safety><needle><![CDATA[origin course anything root arrange furniture dull did speed no actually friend idea idea situation]]><!--corner kitchen her-->-1867850023.2967157</needle><!--completely fear gone extra--><![CDATA[recently famous]]><over does=\"solve\">shinning</over><!--valuable chain chamber week move chamber social diameter appropriate--></establish><compound>official</compound><!--distance--><roof fly=\"love\">medicine</roof><cell>-241215962.018816</cell><![CDATA[manner]]><sight result=\"frighten\">391282223.5958805</sight><!--straw stock write unless--><row>-401856078</row><stiff>parts</stiff><grandmother>1853596079.6196322</grandmother><say cutting=\"solar\">986725992.6229134</say></dig><whale>copper<![CDATA[he age brush shoulder]]><!--jet finest forward--></whale><sweet notice=\"oil\">-1194458674.4927964</sweet><ahead>until</ahead><instrument>it</instrument><recall>-543441378.280621</recall><!--is people--><good>fence</good><![CDATA[join earlier]]></night><darkness>132638461</darkness><!--flight stretch six ten buy feed--><courage>-698899702.4579082</courage><![CDATA[whenever happened]]><newspaper been=\"up\">window</newspaper><create plan=\"growth\">short</create><!--glad--><anywhere>fierce</anywhere><famous health=\"run\">aloud</famous><zoo magnet=\"fog\">998025196</zoo><scene><!--pie-->brother</scene></hill><enjoy experiment=\"halfway\">-1226789123.3947253<![CDATA[control]]></enjoy><gather remove=\"help\">-388341041</gather><![CDATA[knowledge health]]><came><!--syllable building ought storm value-->1187760732</came><leather>-564626049.5221038</leather><!--forget hard army morning rocket add lady whole--></cannot><afraid>bark</afraid><!--none aside--><crop>594584216</crop><pull broken=\"degree\">type</pull><active>1308462200</active><grass>first<!--crew--></grass><serious swing=\"visit\"><!--goose interest signal far collect-->1760686462</serious><needs flight=\"duck\">hunt</needs><victory outer=\"better\">1590411787</victory><mice>-1012352541<![CDATA[seems]]></mice></fact><press>1120375722</press><shown he=\"lay\">1572347724</shown><moon>same</moon><taken>different<!--straw off art ground rapidly split law together--></taken><!--thought price--><bicycle>belt<![CDATA[rays]]></bicycle></equally><seed ring=\"pick\">principal</seed><choice piece=\"brief\"><!--day needs spell-->storm</choice><bell atomic=\"people\">-48426975.90283728<![CDATA[weak quarter differ twice buy company]]></bell><![CDATA[help faster]]><mental>of</mental><feet>72852693</feet><century attention=\"good\">-1972172238</century><order breakfast=\"split\">knew</order><scared>floating</scared><ground>clothing</ground></ranch><gravity happen=\"highway\">-491620514.5753012</gravity><!--wrapped route onlinetools bag tax fear--><rays>-1647811120.7671285</rays><citizen dug=\"reason\"><!--carbon-->fact</citizen><themselves>-237910407.5412097<!--food west generally chapter equally consist or pilot--></themselves><attention>sight</attention><many seldom=\"breath\">-1478570933</many></east><drive try=\"winter\">edge<!--rope judge guide structure village running few--></drive><!--attempt dance customs does--><gather vapor=\"compound\">335878341</gather><mind>heard</mind><![CDATA[eaten uncle guard]]><page>branch</page><finish thick=\"eaten\">rule</finish><!--union nose generally--><rising stretch=\"atmosphere\">medicine</rising><chose mail=\"traffic\">1436719476</chose><recognize ourselves=\"pocket\">stand</recognize><over golden=\"high\"><!--solution-->493352931</over></watch><war><!--struggle-->cookies</war><support>-833036280.9061246</support><!--swing give nodded--><![CDATA[value stop sport planned run]]><football>1403500964.6789079</football><![CDATA[hidden dug]]><tribe cattle=\"center\">1688946869.594727</tribe><doing melted=\"care\">-2114860915.7903452</doing><doctor belt=\"lucky\">1548920291.584949<!--apart strange--></doctor></snow><milk shake=\"discuss\"><![CDATA[city one unless care]]>bit</milk><remarkable>-1452697221<!--add solve--></remarkable><wish>-993489748</wish><![CDATA[speed important escape]]><came>zoo</came><sink chemical=\"bit\">62523342<![CDATA[claws stomach]]></sink><!--nervous price forth lie son serious citizen ate market wave seeing--><they harder=\"nuts\">-1503538167</they><guard hill=\"island\"><![CDATA[flies coal guard dry division]]>2031408346.974905</guard><twice win=\"temperature\">-1585547891.1670504</twice></movie><meal when=\"touch\">either</meal><therefore social=\"captain\"><!--repeat partly--><![CDATA[older]]>-93181736</therefore><nobody leaf=\"tube\">-417292135</nobody><bigger organized=\"hour\">author</bigger><object>-625824694.9381294</object><hundred chapter=\"fifteen\">-929851307.5727658</hundred><relationship><!--sick rough directly-->247373343.05347157</relationship><being pipe=\"goose\">leave<![CDATA[manner born sweet fought]]></being></characteristic><!--step--><universe piano=\"burst\">-99535752.8197093</universe><pencil terrible=\"lower\">1859158080</pencil><![CDATA[foreign customs]]><fellow>1378944178.4665565</fellow><smaller>report</smaller><bad situation=\"quite\">pure<![CDATA[blanket paragraph company]]></bad><activity birds=\"excellent\">-520797743</activity><consonant>1318246036.7974029</consonant></sunlight><!--harder--><solve>industrial</solve><satisfied various=\"understanding\">easily<![CDATA[silly]]></satisfied><far importance=\"number\">-969046248.0550938</far><rhythm farmer=\"invented\">-1638409063.580418</rhythm><electric muscle=\"heavy\">loud</electric><mind tail=\"zoo\"><!--great noun rhythm pick lose built-->freedom</mind><enjoy manner=\"split\">touch</enjoy><iron into=\"numeral\">614433483</iron><if ourselves=\"command\">-1855399679</if></pig><citizen>lot</citizen><terrible kitchen=\"unless\"><![CDATA[nest yes higher warn speak else shaking perfectly thick]]>1111828410.727223</terrible><choice>surface<!--determine mountain--></choice><part again=\"express\">396976584</part><!--herself layers eat--><experience>program</experience><!--general motor chief--><warm>hall</warm><suddenly gave=\"while\">common<![CDATA[supper were social front cook honor mine major soon further welcome]]></suddenly><happen>1479412607</happen></suppose><folks>beauty</folks><meet even=\"split\">-42207104.641019344</meet><![CDATA[garden]]><piano describe=\"naturally\"><![CDATA[ago]]>list</piano><plenty><![CDATA[recognize]]>1714370326</plenty><tank quite=\"wire\">two</tank><![CDATA[share divide]]></chosen><willing><![CDATA[wet unusual selection]]>1737377027</willing><help scale=\"hard\">1658056036</help><![CDATA[paid got]]><being proud=\"tin\">nest</being><size>-1044274697</size><produce mighty=\"zero\"><![CDATA[because]]>once</produce><!--wonderful butter sail push--><choice>lead</choice><past plate=\"white\">prize</past><!--height especially influence--><connected hay=\"married\">captain</connected><![CDATA[what mine pure struggle night]]><copy review=\"led\">ago</copy><!--size block--></play><nest alone=\"underline\">got</nest><question>leader<![CDATA[weigh valley beat body]]></question><office above=\"guide\">capital</office><tree sense=\"change\"><!--soap grow told rubber--><![CDATA[rise]]>system</tree><shape failed=\"scale\"><!--spider pain muscle thou bring-->read</shape><feel darkness=\"arrive\">missing</feel><chicken>limited</chicken></doubt><boy>1775736051.5795598</boy><vast everything=\"black\">position</vast><![CDATA[list darkness dug]]><crew>-1231105386.062221</crew><bit>2075925229.2559695</bit><foreign pot=\"ride\">stiff</foreign><![CDATA[opportunity huge opposite daughter pleasant]]><arm>conversation</arm><spider figure=\"plain\">321821239.0941682</spider></fence><![CDATA[weak die]]><none>house</none><!--serve sum sent--><possibly>labor</possibly><![CDATA[milk wife train west like perhaps]]><bring><!--supper system parts-->thy</bring><afraid>experience</afraid><work>-1679183600</work><!--wise dried jungle doll cow mountain show table direction fair--><mission>-101525349</mission><letter brave=\"potatoes\">draw</letter></hall><![CDATA[sight]]><floor>-644311140</floor><!--care--><upper>knife</upper><came room=\"cross\">traffic</came><tired writing=\"past\">hole<!--fat tales up little space book furniture nose tape--><![CDATA[pack shirt powerful lay unhappy wrote taste zoo]]></tired><drive forgotten=\"flight\">956273229.8374124</drive><farmer spoken=\"anyone\">pole</farmer><prove>eight</prove><whose>-796335224</whose></everyone><slabs would=\"riding\"><![CDATA[chose bark spent nature]]>teacher</slabs><!--tiny design move greater noun crowd were--><![CDATA[imagine of daughter condition]]><mean>sky</mean><!--soap--><out general=\"vessels\">maybe</out><recognize level=\"height\"><!--column-->54651446.58512163</recognize><![CDATA[wire surprise thing duck]]><angle sad=\"lead\">bush</angle><feathers transportation=\"widely\">-429761734.8183894<!--creature--></feathers><lose>1977612113</lose><treated>893385483.4591889</treated><![CDATA[cloth bowl goes instrument has]]><season>1256801419.3063865</season></engineer><nest>magnet</nest><!--myself successful build man winter--><question boat=\"thumb\">earn</question><egg>-826145508.6926465</egg><ever tin=\"tonight\">766099797.0654345</ever><!--gentle ice--><hearing>-293088265</hearing><create recognize=\"stomach\">-1451226882.8711743</create><branch>bush</branch><![CDATA[his palace size globe offer]]><upon swam=\"wall\">certainly</upon></would><cowboy>worse</cowboy><clothes>-1195622647</clothes><slip>angry</slip><ground>-1463492030</ground><additional>991352470</additional><![CDATA[green rich]]><level>rubbed</level><anyway>able</anyway><frequently sweet=\"tank\">sign</frequently><![CDATA[smell fallen slope newspaper police interior meal strike without shine]]><ear cattle=\"appearance\">during</ear></difference><stood record=\"bone\">-268652705.8399224</stood><chapter>sometime</chapter><rod animal=\"opportunity\">-437943999.2643261</rod><![CDATA[something hot president stream slope]]><think wing=\"grown\">perfect</think><search>classroom<![CDATA[safe market feature draw porch touch belt clothing fought scientist]]></search><strange them=\"milk\">factory</strange><over per=\"equator\">-1570290829</over><plenty>-420172283.6172514</plenty><![CDATA[cloth attempt clean completely wind very left women map does further education]]></root>";
            string text = "nativedead11227390261327077737require1809532956origin1062367322.1040087shadowtaken236528695.06021786-1376853916130090471protectionrear1273468858.03536421896621318-110479583.49193096-2092276389-13772997071405717987.85426331976164192forestpostmainlyother1127847599.885682902578978.1991644control1968365931blanketcloth-1804895529.4945512-359101725-931341086told2087375331.228341buy1749364113.06007531220658321.8887482libraryfeltchildren-1665854458302778185.3066845-1633916319nextgulfmen-1275094490-645745896.4873524steel-23557113.8480134-483188206.0902972-1439011160.740409934274528forgottensentencegaveconsonant-1403838342840705289.7410693chooselow-914530815.2269163-294116813.0500617-179262849.28649426-610724434-2128657349-1228738781335748403.445830351379909720trip-560759784.36502171733674009249192283.64177227-972863952.0825939mainly1770745699-236073514shallowwhatever-794529331weekspreadputtingsilvertoolseemsnaturaltiny298586956.66218424796602368.6119423compass1466913286-810502480save1405338855.38274341235320093.60410261120915440.3421664slightnotecompound1380902842.8673573146048458steppedsizejoincallslowlyinstanceacrolinxcontrol1774564338.463005keyverticalcabinsummermotion-1030021265.89565661107853248.9693112eveningblow-1902237267.9245038-783790534.6696854frozencitydeadhusband-1047418329.3654494balance342112045.10907316733004937-1745021008.7245693or166711762respecttraceidentityproduction1502219335fireplacedesert-1052005661400864596.77425575-1002073110endmirror1058628805-8802419751934047579provide-579012910shirt-2090737490.0016646program1304959769farmerprevent768235900.3594475-1427784070-3797477041118558505-1741694151994086412.0788212shown1279233934.4998598fortyringexperience-1523264265.2210135-1867850023.2967157shinningofficialmedicine-241215962.018816391282223.5958805-401856078parts1853596079.6196322986725992.6229134copper-1194458674.4927964untilit-543441378.280621fence132638461-698899702.4579082windowshortfiercealoud998025196brother-1226789123.3947253-3883410411187760732-564626049.5221038bark594584216type1308462200first1760686462hunt1590411787-101235254111203757221572347724samedifferentbeltprincipalstorm-48426975.90283728of72852693-1972172238knewfloatingclothing-491620514.5753012-1647811120.7671285fact-237910407.5412097sight-1478570933edge335878341heardbranchrulemedicine1436719476stand493352931cookies-833036280.90612461403500964.67890791688946869.594727-2114860915.79034521548920291.584949bit-1452697221-993489748zoo62523342-15035381672031408346.974905-1585547891.1670504either-93181736-417292135author-625824694.9381294-929851307.5727658247373343.05347157leave-99535752.819709318591580801378944178.4665565reportpure-5207977431318246036.7974029industrialeasily-969046248.0550938-1638409063.580418loudfreedomtouch614433483-1855399679lot1111828410.727223surface396976584programhallcommon1479412607beauty-42207104.641019344list1714370326two17373770271658056036nest-1044274697onceleadprizecaptainagogotleadercapitalsystemreadmissinglimited1775736051.5795598position-1231105386.0622212075925229.2559695stiffconversation321821239.0941682houselaborthyexperience-1679183600-101525349draw-644311140knifetraffichole956273229.8374124poleeight-796335224teacherskymaybe54651446.58512163bush-429761734.81838941977612113893385483.45918891256801419.3063865magnetearn-826145508.6926465766099797.0654345-293088265-1451226882.8711743bushcertainlyworse-1195622647angry-1463492030991352470rubbedablesignduring-268652705.8399224sometime-437943999.2643261perfectclassroomfactory-1570290829-420172283.6172514";
            string match = "acrolinx";
            int start = xml.IndexOf(match);
            var lookup = new Lookup(xml, Lookup.LookupStrategy.TEXTDIFF);
            lookup.diffOptions.diffInputFormat = DIFF_INPUT_FORMAT.HTML;
            var ranges = new Range[] { new Range(start, start + match.Length) };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = lookup.Search(text, ranges);
            stopwatch.Stop();

            Assert.AreEqual(1, result.Count);

            int expectedStart = text.IndexOf(match);
            int expectedEnd = expectedStart + match.Length;
            Assert.AreEqual(expectedStart, result[0].Start);
            Assert.AreEqual(expectedEnd, result[0].End);

            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 500,
                "Diff took more than 500ms " + stopwatch.ElapsedMilliseconds);
        }
    }
}
