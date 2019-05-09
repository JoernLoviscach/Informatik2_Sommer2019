using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haushaltsbuch
{
    abstract class Buchungsposten
    {
        decimal betrag;
        public decimal Betrag { get { return betrag; } }
        DateTime zeitpunkt;
        public DateTime Zeitpunkt { get { return zeitpunkt; } }
        string beschreibung;
        public string Beschreibung { get { return beschreibung; } }
        public Buchungsposten(decimal betrag, DateTime zeitpunkt, string beschreibung)
        {
            this.betrag = betrag;
            this.zeitpunkt = zeitpunkt;
            this.beschreibung = beschreibung;
        }
        public abstract decimal BerechneGesamtbetragBis(DateTime datum);
    }

    class EinmaligeBuchung : Buchungsposten
    {
        public EinmaligeBuchung(decimal betrag, DateTime zeitpunkt, string beschreibung)
            : base(betrag, zeitpunkt, beschreibung)
        {
        }
        public override decimal BerechneGesamtbetragBis(DateTime datum)
        {
            //if(datum < Zeitpunkt)
            //{
            //    return 0;
            //}
            //return Betrag;
            return (datum < Zeitpunkt) ? 0 : Betrag;
        }
    }

    class MonatlicheBuchung : Buchungsposten
    {
        DateTime endzeitpunkt;
        public MonatlicheBuchung(decimal betrag, DateTime zeitpunkt, DateTime endzeitpunkt, string beschreibung)
            : base(betrag, zeitpunkt, beschreibung)
        {
            this.endzeitpunkt = endzeitpunkt;
        }
        public override decimal BerechneGesamtbetragBis(DateTime datum)
        {
            decimal summe = 0;
            // TODO: Monate durchgehen, nicht immer 30 Tage
            for (DateTime d = Zeitpunkt; d <= endzeitpunkt; d += TimeSpan.FromDays(30))
            {
                if(d <= datum)
                {
                    summe += Betrag;
                }
            }
            return summe;
        }
    }

    class Kontobuch
    {
        List<Buchungsposten> buchungen = new List<Buchungsposten>();
        public Kontobuch()
        {
        }
        public void FügeBuchungHinzu(Buchungsposten buchung)
        {
            buchungen.Add(buchung);
        }
        public decimal BerechneStandAm(DateTime datum)
        {
            decimal kontostand = 0;
            for (int i = 0; i < buchungen.Count; i++)
            {
                kontostand += buchungen[i].BerechneGesamtbetragBis(datum);
            }
            return kontostand;
        }
    }
}
