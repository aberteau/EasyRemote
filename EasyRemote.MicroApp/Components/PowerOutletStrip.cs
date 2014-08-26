using System;
using System.Collections;
using Microsoft.SPOT.Hardware;

namespace Techeasy.EasyRemote.MicroApp.Components
{
    public class PowerOutletStrip
    {
        private readonly Hashtable _outputPinByOutletNumber;

        public PowerOutletStrip()
        {
            _outputPinByOutletNumber = new Hashtable();
        }

        public void AddOutlet(UInt16 number, OutputPort pin)
        {
            _outputPinByOutletNumber.Add(number, pin);
        }

        private OutputPort this[UInt16 outletNumber]
        {
            get
            {
                if (!_outputPinByOutletNumber.Contains(outletNumber))
                    throw new Exception("La prise n°" + outletNumber + " n'existe pas");

                return (OutputPort)_outputPinByOutletNumber[outletNumber];
            }
        }

        public void Write(UInt16 outletNumber, Boolean state)
        {
            this[outletNumber].Write(state);
        }
    }
}
